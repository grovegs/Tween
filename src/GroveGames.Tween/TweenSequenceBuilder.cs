using System;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

public readonly struct TweenSequenceBuilder
{
    private readonly Tweener _tweener;
    private readonly int _slot;
    private readonly int _version;
    private readonly TimeMode _timeMode;
    private readonly ScheduleToken? _token;

    internal TweenSequenceBuilder(Tweener tweener, int slot, int version, TimeMode timeMode, ScheduleToken? token)
    {
        _tweener = tweener;
        _slot = slot;
        _version = version;
        _timeMode = timeMode;
        _token = token;
    }

    public TweenSequenceHandle Handle => new(_tweener, _slot, _version);

    public TweenSequenceBuilder Append<T, TState>(in TweenBuilder<T> tween, TState state, Action<T, TState> setter) where TState : class
    {
        return Add(in tween, state, TweenBuilder<T>.ToUntyped(setter), _tweener.GetSequenceCursor(_slot, _version));
    }

    public TweenSequenceBuilder Join<T, TState>(in TweenBuilder<T> tween, TState state, Action<T, TState> setter) where TState : class
    {
        return Add(in tween, state, TweenBuilder<T>.ToUntyped(setter), _tweener.GetSequenceLastStart(_slot, _version));
    }

    public TweenSequenceBuilder AppendInterval(float interval)
    {
        var cursor = _tweener.GetSequenceCursor(_slot, _version);
        _tweener.PlaceInSequence(_slot, _version, cursor, cursor + Math.Max(interval, 0f), false);
        return this;
    }

    public TweenSequenceBuilder AppendCallback(Action callback)
    {
        if (!_tweener.IsSequenceValid(_slot, _version))
        {
            return this;
        }

        var cursor = _tweener.GetSequenceCursor(_slot, _version);
        _tweener.AddCallback(cursor, _timeMode, callback, _token, _slot, _version);
        _tweener.PlaceInSequence(_slot, _version, cursor, cursor, true);
        return this;
    }

    private TweenSequenceBuilder Add<T>(in TweenBuilder<T> tween, object state, Action<T, object> setter, double start)
    {
        if (!_tweener.IsSequenceValid(_slot, _version))
        {
            return this;
        }

        var child = tween.InSequence(start, _timeMode, _token);
        _tweener.Add(in child, state, setter, _slot, _version);
        _tweener.PlaceInSequence(_slot, _version, start, start + tween.Timing.TotalDuration, true);
        return this;
    }
}
