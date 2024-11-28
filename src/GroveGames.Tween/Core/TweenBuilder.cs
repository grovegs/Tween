using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

public readonly struct TweenBuilder
{
    private readonly ITween _tween;

    internal TweenBuilder(in ITween tween)
    {
        _tween = tween;
    }

    public readonly TweenBuilder Ease(EaseType easeType)
    {
        _tween.SetEase(easeType);
        return this;
    }

    public readonly TweenBuilder OnComplete(Action onComplete)
    {
        _tween.SetOnComplete(onComplete);
        return this;
    }

    public readonly TweenBuilder Pause()
    {
        _tween.Pause();
        return this;
    }

    public readonly TweenBuilder Play()
    {
        _tween.Play();
        return this;
    }

    public readonly TweenBuilder Stop(bool complete)
    {
        _tween.Stop(complete);
        return this;
    }

    public readonly TweenBuilder OnUpdate<T>(Action<T> onUpdate)
    {
        ((ITween<T>)_tween).SetOnUpdate(onUpdate);
        return this;
    }
}
