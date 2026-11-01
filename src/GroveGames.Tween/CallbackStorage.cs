using System;
using System.Runtime.CompilerServices;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

internal sealed class CallbackStorage
{
    private readonly Tweener _tweener;
    private CallbackEntry[] _entries;
    private int _count;

    public CallbackStorage(Tweener tweener, int capacity)
    {
        _tweener = tweener;
        _entries = new CallbackEntry[capacity];
    }

    public void Add(double delay, TimeMode timeMode, Action callback, ScheduleToken? token, int sequenceSlot, int sequenceVersion)
    {
        if (_count == _entries.Length)
        {
            Array.Resize(ref _entries, _count * 2);
        }

        _entries[_count++] = new CallbackEntry(delay, timeMode, callback, token, sequenceSlot, sequenceVersion);
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

            if (entry.Advance(deltaTimes[(int)entry.TimeMode]))
            {
                var callback = entry.Callback;
                Kill(ref entry);
                TryInvoke(callback);
                continue;
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

    private void TryInvoke(Action callback)
    {
        try
        {
            callback.Invoke();
        }
        catch (Exception exception)
        {
            _tweener.HandleException(exception);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsCanceled(ref CallbackEntry entry)
    {
        return entry.IsTokenCanceled || !_tweener.IsSequenceValid(entry.SequenceSlot, entry.SequenceVersion);
    }

    private void Kill(ref CallbackEntry entry)
    {
        entry.Kill();
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
    }
}
