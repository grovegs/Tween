using System;
using System.Runtime.CompilerServices;

namespace GroveGames.Tween;

internal readonly struct TweenTiming
{
    private readonly float _duration;
    private readonly float _delay;
    private readonly int _loops;
    private readonly LoopType _loopType;
    private readonly float _loopInterval;
    private readonly Ease _ease;
    private readonly TweenKind _kind;
    private readonly int _frequency;
    private readonly float _dampingRatio;
    private readonly double _period;
    private readonly double _activeDuration;

    public TweenTiming(float duration, float delay, int loops, LoopType loopType, float loopInterval, Ease ease, TweenKind kind, int frequency, float dampingRatio)
    {
        _duration = Math.Max(duration, 0f);
        _delay = Math.Max(delay, 0f);
        _loops = loops == 0 ? 1 : Math.Max(loops, -1);
        _loopType = loopType;
        _loopInterval = Math.Max(loopInterval, 0f);
        _ease = ease;
        _kind = kind;
        _frequency = frequency;
        _dampingRatio = dampingRatio;
        _period = (double)_duration + _loopInterval;
        _activeDuration = _loops < 0 ? double.PositiveInfinity : (double)_duration * _loops + (double)_loopInterval * (_loops - 1);
    }

    public bool IsInfinite => _loops < 0;
    public double TotalDuration => _delay + _activeDuration;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TweenStatus Evaluate(double time, out float progress)
    {
        var localTime = time - _delay;

        if (localTime < 0d)
        {
            progress = 0f;
            return TweenStatus.Delayed;
        }

        if (_duration <= 0f)
        {
            progress = EvaluateEnd();
            return IsInfinite ? TweenStatus.Playing : TweenStatus.Completed;
        }

        if (localTime >= _activeDuration)
        {
            progress = EvaluateEnd();
            return TweenStatus.Completed;
        }

        if (localTime < _duration)
        {
            progress = Shape(Easing.Evaluate(_ease, (float)(localTime / _duration)));
            return TweenStatus.Playing;
        }

        var loop = (long)(localTime / _period);
        var t = (float)Math.Min((localTime - loop * _period) / _duration, 1d);
        var isReversed = _loopType == LoopType.Yoyo && (loop & 1) == 1;
        progress = Shape(Easing.Evaluate(_ease, isReversed ? 1f - t : t));
        return TweenStatus.Playing;
    }

    private float EvaluateEnd()
    {
        var isReversed = _loopType == LoopType.Yoyo && !IsInfinite && _loops % 2 == 0;
        return Shape(Easing.Evaluate(_ease, isReversed ? 0f : 1f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float Shape(float progress)
    {
        if (_kind == TweenKind.Interpolation)
        {
            return progress;
        }

        if (progress == 0f || progress == 1f)
        {
            return 0f;
        }

        var angularFrequency = (_frequency * progress - 0.5f) * MathF.PI;
        var damping = _dampingRatio * _frequency / (2f * MathF.PI);
        return MathF.Exp(-damping * progress) * MathF.Cos(angularFrequency);
    }
}
