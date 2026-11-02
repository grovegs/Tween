using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

internal sealed class TweenStorage<T, TAdapter> : ITweenValueStorage<T> where TAdapter : struct, ITweenAdapter<T>
{
    private readonly Tweener _tweener;
    private readonly SlotTable _slots;
    private readonly int _id;
    private TweenEntry<T>[] _entries;
    private int _count;

    public TweenStorage(Tweener tweener, int id, int capacity)
    {
        _tweener = tweener;
        _slots = new SlotTable(capacity);
        _id = id;
        _entries = new TweenEntry<T>[capacity];
    }

    public int Id => _id;

    public int Add(T from, T to, in TweenTiming timing, TimeMode timeMode, object state, Action<T, object> setter, Action? completed, IReadOnlyList<T>? points, ScheduleToken? token, int sequenceSlot, int sequenceVersion, out int version)
    {
        if (_count == _entries.Length)
        {
            Array.Resize(ref _entries, _count * 2);
        }

        var index = _count++;
        var slot = _slots.Allocate(index, out version);
        _entries[index] = new TweenEntry<T>(from, to, in timing, timeMode, state, setter, completed, points, token, sequenceSlot, sequenceVersion, slot);

        if (timing.Evaluate(0d, out var progress) != TweenStatus.Delayed)
        {
            setter(Evaluate(in _entries[index], progress), state);
        }

        return slot;
    }

    public void Update(float[] deltaTimes)
    {
        var count = _count;
        var write = 0;

        for (var read = 0; read < count; read++)
        {
            ref var entry = ref _entries[read];

            if (!entry.IsAlive)
            {
                continue;
            }

            if (IsCanceled(ref entry))
            {
                Kill(ref entry);
                continue;
            }

            var status = entry.Advance(deltaTimes[(int)entry.TimeMode], out var progress);

            if (status != TweenStatus.Delayed)
            {
                var value = Evaluate(in entry, progress);
                var isApplied = TryInvoke(entry.Setter, value, entry.State);
                entry = ref _entries[read];

                if (!entry.IsAlive)
                {
                    continue;
                }

                if (!isApplied)
                {
                    Kill(ref entry);
                    continue;
                }

                if (status == TweenStatus.Completed)
                {
                    var completed = entry.Completed;
                    Kill(ref entry);
                    TryInvoke(completed);
                    continue;
                }
            }

            Move(read, write++);
        }

        for (var read = count; read < _count; read++)
        {
            if (_entries[read].IsAlive)
            {
                Move(read, write++);
            }
        }

        Array.Clear(_entries, write, _count - write);
        _count = write;
    }

    public bool IsActive(int slot, int version)
    {
        return _slots.IsValid(slot, version) && !IsCanceled(ref _entries[_slots.GetValue(slot)]);
    }

    public bool Cancel(int slot, int version)
    {
        if (!_slots.IsValid(slot, version))
        {
            return false;
        }

        ref var entry = ref _entries[_slots.GetValue(slot)];
        var isCanceled = IsCanceled(ref entry);
        Kill(ref entry);
        return !isCanceled;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T Evaluate(in TweenEntry<T> entry, float progress)
    {
        var points = entry.Points;

        if (points == null)
        {
            return default(TAdapter).Evaluate(entry.From, entry.To, progress);
        }

        var segmentCount = points.Count + 1;
        var position = Math.Clamp(progress, 0f, 1f) * segmentCount;
        var segment = Math.Min((int)position, segmentCount - 1);
        var start = segment == 0 ? entry.From : points[segment - 1];
        var end = segment == points.Count ? entry.From : points[segment];
        return default(TAdapter).Evaluate(start, end, position - segment);
    }

    private bool TryInvoke(Action<T, object> setter, T value, object state)
    {
        try
        {
            setter(value, state);
            return true;
        }
        catch (Exception exception)
        {
            _tweener.HandleException(exception);
            return false;
        }
    }

    private void TryInvoke(Action? callback)
    {
        try
        {
            callback?.Invoke();
        }
        catch (Exception exception)
        {
            _tweener.HandleException(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsCanceled(ref TweenEntry<T> entry)
    {
        return entry.IsTokenCanceled || (entry.SequenceSlot >= 0 && !_tweener.IsSequenceValid(entry.SequenceSlot, entry.SequenceVersion));
    }

    private void Kill(ref TweenEntry<T> entry)
    {
        entry.Kill();
        _slots.Release(entry.Slot);
        _tweener.ReleaseSequenceChild(entry.SequenceSlot, entry.SequenceVersion);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Move(int from, int to)
    {
        if (from == to)
        {
            return;
        }

        _entries[to] = _entries[from];
        _entries[from] = default;
        _slots.SetValue(_entries[to].Slot, to);
    }
}
