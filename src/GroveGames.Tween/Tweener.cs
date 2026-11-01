using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

public sealed class Tweener : ITweener
{
    private const int StorageCapacity = 16;
    private const int StorageTypeCapacity = 4;
    private const int SequenceCapacity = 8;
    private const int TimeModeCount = 3;

    private readonly Action<Exception> _exceptionHandler;
    private readonly float[] _deltaTimes;
    private readonly TweenStorage<float, FloatTweenAdapter> _floats;
    private readonly CallbackStorage _callbacks;
    private readonly SlotTable _sequences;
    private ITweenStorage?[] _storagesById;
    private ITweenStorage[] _storages;
    private int _storageCount;
    private double[] _sequenceCursors;
    private double[] _sequenceLastStarts;
    private int[] _pendingSequenceSlots;
    private int[] _pendingSequenceVersions;
    private int _pendingSequenceCount;

    public Tweener(Action<Exception> exceptionHandler)
    {
        ArgumentNullException.ThrowIfNull(exceptionHandler);

        _exceptionHandler = exceptionHandler;
        _deltaTimes = new float[TimeModeCount];
        _storagesById = new ITweenStorage?[StorageTypeCapacity];
        _storages = new ITweenStorage[StorageTypeCapacity];
        _storageCount = 0;
        _floats = GetStorage<float, FloatTweenAdapter>();
        _callbacks = new CallbackStorage(this, StorageCapacity);
        _sequences = new SlotTable(SequenceCapacity);
        _sequenceCursors = new double[SequenceCapacity];
        _sequenceLastStarts = new double[SequenceCapacity];
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
        return new(this, GetStorage<T, TAdapter>(), from, to, new TweenSettings(duration), TweenKind.Interpolation, 0, 0f, null);
    }

    public TweenBuilder<T> Create<T, TAdapter>(T from, T to, in TweenSettings settings) where TAdapter : struct, ITweenAdapter<T>
    {
        return new(this, GetStorage<T, TAdapter>(), from, to, in settings, TweenKind.Interpolation, 0, 0f, null);
    }

    public TweenBuilder<T> CreatePunch<T, TAdapter>(T from, T to, float duration, int frequency = 10, float dampingRatio = 1f) where TAdapter : struct, ITweenAdapter<T>
    {
        return new(this, GetStorage<T, TAdapter>(), from, to, new TweenSettings(duration), TweenKind.Punch, frequency, dampingRatio, null);
    }

    public TweenBuilder<T> CreateShake<T, TAdapter>(T from, IReadOnlyList<T> points, float duration) where TAdapter : struct, ITweenAdapter<T>
    {
        return new(this, GetStorage<T, TAdapter>(), from, from, new TweenSettings(duration), TweenKind.Interpolation, 0, 0f, points);
    }

    public TweenSequenceBuilder CreateSequence(TimeMode timeMode = TimeMode.Scaled, ScheduleToken? token = null)
    {
        var slot = _sequences.Allocate(0, out var version);

        if (_sequenceCursors.Length < _sequences.Capacity)
        {
            Array.Resize(ref _sequenceCursors, _sequences.Capacity);
            Array.Resize(ref _sequenceLastStarts, _sequences.Capacity);
        }

        _sequenceCursors[slot] = 0d;
        _sequenceLastStarts[slot] = 0d;

        if (_pendingSequenceCount == _pendingSequenceSlots.Length)
        {
            Array.Resize(ref _pendingSequenceSlots, _pendingSequenceCount * 2);
            Array.Resize(ref _pendingSequenceVersions, _pendingSequenceCount * 2);
        }

        _pendingSequenceSlots[_pendingSequenceCount] = slot;
        _pendingSequenceVersions[_pendingSequenceCount] = version;
        _pendingSequenceCount++;
        return new TweenSequenceBuilder(this, slot, version, timeMode, token);
    }

    public void Update(float deltaTime, float unscaledDeltaTime, float realtimeDeltaTime)
    {
        ReleaseEmptySequences();
        _deltaTimes[(int)TimeMode.Scaled] = deltaTime;
        _deltaTimes[(int)TimeMode.Unscaled] = unscaledDeltaTime;
        _deltaTimes[(int)TimeMode.Realtime] = realtimeDeltaTime;

        for (var i = 0; i < _storageCount; i++)
        {
            _storages[i].Update(_deltaTimes);
        }

        _callbacks.Update(_deltaTimes);
    }

    internal TweenHandle Add<T>(in TweenBuilder<T> builder, object state, Action<T, object> setter, int sequenceSlot, int sequenceVersion)
    {
        var token = builder.Token;

        if (token != null && token.IsCanceled)
        {
            return default;
        }

        AddSequenceChild(sequenceSlot, sequenceVersion);
        var storage = builder.Storage;
        var slot = storage.Add(builder.From, builder.To, builder.Timing, builder.TimeMode, state, setter, builder.CompletionCallback, builder.Points, token, sequenceSlot, sequenceVersion, out var version);
        return new TweenHandle(this, storage.Id, slot, version);
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

    internal bool IsActive(int storageId, int slot, int version)
    {
        return _storagesById[storageId]!.IsActive(slot, version);
    }

    internal bool Cancel(int storageId, int slot, int version)
    {
        return _storagesById[storageId]!.Cancel(slot, version);
    }

    internal bool IsSequenceValid(int slot, int version)
    {
        return _sequences.IsValid(slot, version);
    }

    internal bool IsSequenceActive(int slot, int version)
    {
        return _sequences.IsValid(slot, version) && _sequences.GetValue(slot) > 0;
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

        _sequences.Release(slot);
    }

    internal bool CancelSequence(int slot, int version)
    {
        if (!IsSequenceActive(slot, version))
        {
            return false;
        }

        _sequences.Release(slot);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TweenStorage<T, TAdapter> GetStorage<T, TAdapter>() where TAdapter : struct, ITweenAdapter<T>
    {
        var id = TweenStorageId<T, TAdapter>.Value;

        if (id < _storagesById.Length)
        {
            var storage = _storagesById[id];

            if (storage != null)
            {
                return Unsafe.As<TweenStorage<T, TAdapter>>(storage);
            }
        }

        return AddStorage<T, TAdapter>(id);
    }

    private TweenStorage<T, TAdapter> AddStorage<T, TAdapter>(int id) where TAdapter : struct, ITweenAdapter<T>
    {
        if (id >= _storagesById.Length)
        {
            Array.Resize(ref _storagesById, Math.Max(id + 1, _storagesById.Length * 2));
        }

        if (_storageCount == _storages.Length)
        {
            Array.Resize(ref _storages, _storageCount * 2);
        }

        var storage = new TweenStorage<T, TAdapter>(this, id, StorageCapacity);
        _storagesById[id] = storage;
        _storages[_storageCount++] = storage;
        return storage;
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
                _sequences.Release(slot);
            }
        }

        _pendingSequenceCount = 0;
    }
}
