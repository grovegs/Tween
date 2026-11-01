using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

internal struct TweenEntry<T>
{
    private readonly T _from;
    private readonly T _to;
    private readonly TweenTiming _timing;
    private readonly TimeMode _timeMode;
    private readonly object _state;
    private readonly Action<T, object> _setter;
    private readonly Action? _completed;
    private readonly IReadOnlyList<T>? _points;
    private readonly ScheduleToken? _token;
    private readonly int _tokenVersion;
    private readonly int _sequenceSlot;
    private readonly int _sequenceVersion;
    private readonly int _slot;
    private double _time;
    private bool _isAlive;

    public TweenEntry(T from, T to, in TweenTiming timing, TimeMode timeMode, object state, Action<T, object> setter, Action? completed, IReadOnlyList<T>? points, ScheduleToken? token, int sequenceSlot, int sequenceVersion, int slot)
    {
        _from = from;
        _to = to;
        _timing = timing;
        _timeMode = timeMode;
        _state = state;
        _setter = setter;
        _completed = completed;
        _points = points;
        _token = token;
        _tokenVersion = token != null ? token.Version : 0;
        _sequenceSlot = sequenceSlot;
        _sequenceVersion = sequenceVersion;
        _slot = slot;
        _time = 0d;
        _isAlive = true;
    }

    public readonly T From => _from;
    public readonly T To => _to;
    public readonly TweenTiming Timing => _timing;
    public readonly TimeMode TimeMode => _timeMode;
    public readonly object State => _state;
    public readonly Action<T, object> Setter => _setter;
    public readonly Action? Completed => _completed;
    public readonly IReadOnlyList<T>? Points => _points;
    public readonly int SequenceSlot => _sequenceSlot;
    public readonly int SequenceVersion => _sequenceVersion;
    public readonly int Slot => _slot;
    public readonly bool IsAlive => _isAlive;
    public readonly bool IsTokenCanceled => _token != null && _token.IsCanceledSince(_tokenVersion);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TweenStatus Advance(float deltaTime, out float progress)
    {
        _time += deltaTime;
        return _timing.Evaluate(_time, out progress);
    }

    public void Kill()
    {
        _isAlive = false;
    }
}
