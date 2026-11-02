using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GroveGames.Scheduling;
using JetBrains.Annotations;

namespace GroveGames.Tween;

public struct TweenBuilder<T>
{
    private readonly Tweener _tweener;
    private readonly ITweenValueStorage<T> _storage;
    private readonly T _from;
    private readonly T _to;
    private readonly float _duration;
    private readonly TweenKind _kind;
    private readonly int _frequency;
    private readonly float _dampingRatio;
    private readonly IReadOnlyList<T>? _points;
    private Ease _ease;
    private float _delay;
    private int _loops;
    private LoopType _loopType;
    private float _loopInterval;
    private TimeMode _timeMode;
    private ScheduleToken? _token;
    private Action? _completionCallback;

    internal readonly ITweenValueStorage<T> Storage => _storage;
    internal readonly T From => _from;
    internal readonly T To => _to;
    internal readonly TimeMode TimeMode => _timeMode;
    internal readonly ScheduleToken? Token => _token;
    internal readonly IReadOnlyList<T>? Points => _points;
    internal readonly Action? CompletionCallback => _completionCallback;
    internal readonly TweenTiming Timing => new(_duration, _delay, _loops, _loopType, _loopInterval, _ease, _kind, _frequency, _dampingRatio);

    internal TweenBuilder(Tweener tweener, ITweenValueStorage<T> storage, T from, T to, in TweenSettings settings, TweenKind kind, int frequency, float dampingRatio, IReadOnlyList<T>? points)
    {
        _tweener = tweener;
        _storage = storage;
        _from = from;
        _to = to;
        _duration = settings.Duration;
        _kind = kind;
        _frequency = frequency;
        _dampingRatio = dampingRatio;
        _points = points;
        _ease = settings.Ease;
        _delay = settings.Delay;
        _loops = settings.Loops;
        _loopType = settings.LoopType;
        _loopInterval = settings.LoopInterval;
        _timeMode = settings.TimeMode;
        _token = null;
        _completionCallback = null;
    }

    [MustUseReturnValue]
    public readonly TweenBuilder<T> WithEase(Ease ease)
    {
        var builder = this;
        builder._ease = ease;
        return builder;
    }

    [MustUseReturnValue]
    public readonly TweenBuilder<T> WithDelay(float delay)
    {
        var builder = this;
        builder._delay = delay;
        return builder;
    }

    [MustUseReturnValue]
    public readonly TweenBuilder<T> WithLoops(int loops, LoopType loopType = LoopType.Restart, float interval = 0f)
    {
        var builder = this;
        builder._loops = loops;
        builder._loopType = loopType;
        builder._loopInterval = interval;
        return builder;
    }

    [MustUseReturnValue]
    public readonly TweenBuilder<T> WithTimeMode(TimeMode timeMode)
    {
        var builder = this;
        builder._timeMode = timeMode;
        return builder;
    }

    [MustUseReturnValue]
    public readonly TweenBuilder<T> WithToken(ScheduleToken token)
    {
        var builder = this;
        builder._token = token;
        return builder;
    }

    [MustUseReturnValue]
    public readonly TweenBuilder<T> WithCompletionCallback(Action completionCallback)
    {
        var builder = this;
        builder._completionCallback = completionCallback;
        return builder;
    }

    public readonly TweenHandle Bind<TState>(TState state, Action<T, TState> setter) where TState : class
    {
        return _tweener.Add(in this, state, ToUntyped(setter), -1, 0);
    }

    internal readonly TweenBuilder<T> InSequence(double start, TimeMode timeMode, ScheduleToken? token)
    {
        var builder = this;
        builder._delay = (float)(start + _delay);
        builder._timeMode = timeMode;
        builder._token = token ?? _token;
        return builder;
    }

    internal static Action<T, object> ToUntyped<TState>(Action<T, TState> setter) where TState : class
    {
        return Unsafe.As<Action<T, object>>(setter);
    }
}
