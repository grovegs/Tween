using System;
using System.Runtime.CompilerServices;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

internal struct CallbackEntry
{
    private readonly double _delay;
    private readonly TimeMode _timeMode;
    private readonly Action _callback;
    private readonly ScheduleToken? _token;
    private readonly int _tokenVersion;
    private readonly int _sequenceSlot;
    private readonly int _sequenceVersion;
    private double _time;
    private bool _isAlive;

    public CallbackEntry(double delay, TimeMode timeMode, Action callback, ScheduleToken? token, int sequenceSlot, int sequenceVersion)
    {
        _delay = Math.Max(delay, 0d);
        _timeMode = timeMode;
        _callback = callback;
        _token = token;
        _tokenVersion = token != null ? token.Version : 0;
        _sequenceSlot = sequenceSlot;
        _sequenceVersion = sequenceVersion;
        _time = 0d;
        _isAlive = true;
    }

    public readonly TimeMode TimeMode => _timeMode;
    public readonly Action Callback => _callback;
    public readonly int SequenceSlot => _sequenceSlot;
    public readonly int SequenceVersion => _sequenceVersion;
    public readonly bool IsAlive => _isAlive;
    public readonly bool IsTokenCanceled => _token != null && _token.IsCanceledSince(_tokenVersion);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Advance(float deltaTime)
    {
        _time += deltaTime;
        return _time >= _delay;
    }

    public void Kill()
    {
        _isAlive = false;
    }
}
