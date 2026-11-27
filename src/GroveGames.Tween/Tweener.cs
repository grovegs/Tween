using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

public sealed class Tweener : ITweener
{
    private const int ListCapacity = 16;
    private const int ListTypeCapacity = 4;
    private const int SequenceCapacity = 8;
    private const int TimeModeCount = 3;

    private readonly Action<Exception> _exceptionHandler;
    private readonly float[] _deltaTimes;
    private readonly TweenList<float, FloatTweenAdapter> _floats;
    private readonly CallbackList _callbacks;
    private readonly SlotTable _sequences;
    private ITweenList?[] _listsById;
    private ITweenList[] _lists;
    private int[] _listCounts;
    private int _listCount;
    private double[] _sequenceCursors;
    private double[] _sequenceLastStarts;
    private ScheduleToken?[] _sequenceTokens;
    private int[] _sequenceTokenVersions;
    private int[] _pendingSequenceSlots;
    private int[] _pendingSequenceVersions;
    private int _pendingSequenceCount;

    public Tweener(Action<Exception> exceptionHandler)
    {
        ArgumentNullException.ThrowIfNull(exceptionHandler);

        _exceptionHandler = exceptionHandler;
        _deltaTimes = new float[TimeModeCount];
        _listsById = new ITweenList?[ListTypeCapacity];
        _lists = new ITweenList[ListTypeCapacity];
        _listCounts = new int[ListTypeCapacity];
        _listCount = 0;
        _floats = GetList<float, FloatTweenAdapter>();
        _callbacks = new CallbackList(this, ListCapacity);
        _sequences = new SlotTable(SequenceCapacity);
        _sequenceCursors = new double[SequenceCapacity];
        _sequenceLastStarts = new double[SequenceCapacity];
        _sequenceTokens = new ScheduleToken?[SequenceCapacity];
        _sequenceTokenVersions = new int[SequenceCapacity];
        _pendingSequenceSlots = new int[SequenceCapacity];
        _pendingSequenceVersions = new int[SequenceCapacity];
        _pendingSequenceCount = 0;
    }

    public TweenBuilder<float> Create(float from, float to, float duration)
    {
        return new(this, _floats, from, to, new TweenSettings(duration), TweenKind.Interpolation, 0, 0f, null);
    }

    public TweenBuilder<float> Create(float from, float to, in TweenSettings settings)
    {
        return new(this, _floats, from, to, in settings, TweenKind.Interpolation, 0, 0f, null);
    }

    public TweenBuilder<float> CreateShake(float from, IReadOnlyList<float> points, float duration)
    {
        return new(this, _floats, from, from, new TweenSettings(duration), TweenKind.Interpolation, 0, 0f, points);
    }

    public TweenBuilder<T> Create<T, TAdapter>(T from, T to, float duration) where TAdapter : struct, ITweenAdapter<T>
    {
        return new(this, GetList<T, TAdapter>(), from, to, new TweenSettings(duration), TweenKind.Interpolation, 0, 0f, null);
    }

    public TweenBuilder<T> Create<T, TAdapter>(T from, T to, in TweenSettings settings) where TAdapter : struct, ITweenAdapter<T>
    {
        return new(this, GetList<T, TAdapter>(), from, to, in settings, TweenKind.Interpolation, 0, 0f, null);
    }

    public TweenBuilder<T> CreatePunch<T, TAdapter>(T from, T to, float duration, int frequency = 10, float dampingRatio = 1f) where TAdapter : struct, ITweenAdapter<T>
    {
        return new(this, GetList<T, TAdapter>(), from, to, new TweenSettings(duration), TweenKind.Punch, frequency, dampingRatio, null);
    }

    public TweenBuilder<T> CreateShake<T, TAdapter>(T from, IReadOnlyList<T> points, float duration) where TAdapter : struct, ITweenAdapter<T>
    {
        return new(this, GetList<T, TAdapter>(), from, from, new TweenSettings(duration), TweenKind.Interpolation, 0, 0f, points);
    }

    public TweenSequenceBuilder CreateSequence(TimeMode timeMode = TimeMode.Scaled, ScheduleToken? token = null)
    {
        var slot = _sequences.Allocate(0, out var version);

        if (_sequenceCursors.Length < _sequences.Capacity)
        {
            Array.Resize(ref _sequenceCursors, _sequences.Capacity);
            Array.Resize(ref _sequenceLastStarts, _sequences.Capacity);
            Array.Resize(ref _sequenceTokens, _sequences.Capacity);
            Array.Resize(ref _sequenceTokenVersions, _sequences.Capacity);
        }

        _sequenceCursors[slot] = 0d;
        _sequenceLastStarts[slot] = 0d;
        _sequenceTokens[slot] = token;
        _sequenceTokenVersions[slot] = token != null ? token.Version : 0;

        if (_pendingSequenceCount == _pendingSequenceSlots.Length)
        {
            Array.Resize(ref _pendingSequenceSlots, _pendingSequenceCount * 2);
            Array.Resize(ref _pendingSequenceVersions, _pendingSequenceCount * 2);
        }

        _pendingSequenceSlots[_pendingSequenceCount] = slot;
        _pendingSequenceVersions[_pendingSequenceCount] = version;
        _pendingSequenceCount++;
        return new TweenSequenceBuilder(this, slot, version, timeMode);
    }

    public void Update(float deltaTime, float unscaledDeltaTime, float realtimeDeltaTime)
    {
        ReleaseEmptySequences();
        _deltaTimes[(int)TimeMode.Scaled] = deltaTime;
        _deltaTimes[(int)TimeMode.Unscaled] = unscaledDeltaTime;
        _deltaTimes[(int)TimeMode.Realtime] = realtimeDeltaTime;

        var listCount = _listCount;
        var callbackCount = _callbacks.Count;

        for (var i = 0; i < listCount; i++)
        {
            _listCounts[i] = _lists[i].Count;
        }

        for (var i = 0; i < listCount; i++)
        {
            _lists[i].Update(_deltaTimes, _listCounts[i]);
        }

        _callbacks.Update(_deltaTimes, callbackCount);
    }

    internal TweenHandle Add<T>(in TweenBuilder<T> builder, object state, Action<T, object> setter, int sequenceSlot, int sequenceVersion)
    {
        var timing = builder.Timing;
        var token = builder.Token;

        if (token != null && token.IsCanceled)
        {
            return default;
        }

        AddSequenceChild(sequenceSlot, sequenceVersion);
        var list = builder.TweenList;
        var slot = list.Add(builder.From, builder.To, in timing, builder.TimeMode, state, setter, builder.CompletionCallback, builder.Points, token, sequenceSlot, sequenceVersion, out var version);
        return new TweenHandle(this, list.Id, slot, version);
    }

    internal void AddCallback(double delay, TimeMode timeMode, Action callback, ScheduleToken? token, int sequenceSlot, int sequenceVersion)
    {
        if (token != null && token.IsCanceled)
        {
            return;
        }

        AddSequenceChild(sequenceSlot, sequenceVersion);
        _callbacks.Add(delay, timeMode, callback, token, sequenceSlot, sequenceVersion);
    }

    internal void HandleException(Exception exception)
    {
        _exceptionHandler(exception);
    }

    internal bool IsActive(int listId, int slot, int version)
    {
        return _listsById[listId]!.IsActive(slot, version);
    }

    internal bool Cancel(int listId, int slot, int version)
    {
        return _listsById[listId]!.Cancel(slot, version);
    }

    internal bool IsSequenceRunning(int slot, int version)
    {
        if (!_sequences.IsValid(slot, version))
        {
            return false;
        }

        var token = _sequenceTokens[slot];
        return token == null || !token.IsCanceledSince(_sequenceTokenVersions[slot]);
    }

    internal bool IsSequenceActive(int slot, int version)
    {
        return IsSequenceRunning(slot, version) && _sequences.GetValue(slot) > 0;
    }

    internal double GetSequenceCursor(int slot, int version)
    {
        return _sequences.IsValid(slot, version) ? _sequenceCursors[slot] : 0d;
    }

    internal double GetSequenceLastStart(int slot, int version)
    {
        return _sequences.IsValid(slot, version) ? _sequenceLastStarts[slot] : 0d;
    }

    internal void PlaceInSequence(int slot, int version, double start, double end, bool isAnchor)
    {
        if (!_sequences.IsValid(slot, version))
        {
            return;
        }

        _sequenceCursors[slot] = Math.Max(_sequenceCursors[slot], end);

        if (isAnchor)
        {
            _sequenceLastStarts[slot] = start;
        }
    }

    internal void ReleaseSequenceChild(int slot, int version)
    {
        if (!_sequences.IsValid(slot, version))
        {
            return;
        }

        var remaining = _sequences.GetValue(slot) - 1;

        if (remaining > 0)
        {
            _sequences.SetValue(slot, remaining);
            return;
        }

        Release(slot);
    }

    internal bool CancelSequence(int slot, int version)
    {
        if (!IsSequenceActive(slot, version))
        {
            return false;
        }

        Release(slot);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TweenList<T, TAdapter> GetList<T, TAdapter>() where TAdapter : struct, ITweenAdapter<T>
    {
        var id = TweenListId<T, TAdapter>.Value;

        if (id < _listsById.Length)
        {
            var list = _listsById[id];

            if (list != null)
            {
                return Unsafe.As<TweenList<T, TAdapter>>(list);
            }
        }

        return AddList<T, TAdapter>(id);
    }

    private TweenList<T, TAdapter> AddList<T, TAdapter>(int id) where TAdapter : struct, ITweenAdapter<T>
    {
        if (id >= _listsById.Length)
        {
            Array.Resize(ref _listsById, Math.Max(id + 1, _listsById.Length * 2));
        }

        if (_listCount == _lists.Length)
        {
            Array.Resize(ref _lists, _listCount * 2);
            Array.Resize(ref _listCounts, _listCount * 2);
        }

        var list = new TweenList<T, TAdapter>(this, id, ListCapacity);
        _listsById[id] = list;
        _lists[_listCount++] = list;
        return list;
    }

    private void Release(int slot)
    {
        _sequenceTokens[slot] = null;
        _sequences.Release(slot);
    }

    private void AddSequenceChild(int slot, int version)
    {
        if (_sequences.IsValid(slot, version))
        {
            _sequences.SetValue(slot, _sequences.GetValue(slot) + 1);
        }
    }

    private void ReleaseEmptySequences()
    {
        for (var i = 0; i < _pendingSequenceCount; i++)
        {
            var slot = _pendingSequenceSlots[i];

            if (_sequences.IsValid(slot, _pendingSequenceVersions[i]) && _sequences.GetValue(slot) == 0)
            {
                Release(slot);
            }
        }

        _pendingSequenceCount = 0;
    }
}
