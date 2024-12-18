using GroveGames.Tween.Core;
using GroveGames.Tween.Pooling;

namespace GroveGames.Tween.Context;

public class TweenerContext
{
    private readonly List<ITween> _tweens;
    private readonly HashSet<ITween> _stoppedTweens;
    private readonly TweenPool _tweenPool;

    public TweenerContext()
    {
        _tweens = [];
        _stoppedTweens = [];
        _tweenPool = new TweenPool();
    }

    public ITween<T> CreateTween<T>(Func<T> start, Func<T> end, float duration, Func<T, T, float, T> lerpFunc, bool autoPlay)
    {
        var tween = _tweenPool.Get<T>();
        var tweenInstance = (Tween<T>)tween;
        tweenInstance.Construct(start, end, duration, lerpFunc, autoPlay);
        tween.SetOnComplete(() => _stoppedTweens.Add(tween));
        _tweens.Add(tween);

        return tweenInstance;
    }

    public void Stop(ITween tween)
    {
        _stoppedTweens.Add(tween);
    }

    public void Stop(int id)
    {
        foreach (var tween in _tweens)
        {
            if (tween.Id == id)
            {
                Stop(tween);
                break;
            }
        }
    }

    public ISequence CreateSequnce()
    {
        var sequence = _tweenPool.Get<ISequence>();
        sequence.SetOnComplete(() => _stoppedTweens.Add(sequence));
        _tweens.Add(sequence);

        return (ISequence)sequence;
    }

    public void Update(float deltaTime)
    {
        for (var i = _tweens.Count - 1; i >= 0; i--)
        {
            var tween = _tweens[i];

            if (_stoppedTweens.Contains(tween))
            {
                _stoppedTweens.Remove(tween);
                _tweens.RemoveAt(i);
                tween.Stop(false);
                continue;
            }

            if (!tween.IsRunning)
            {
                _stoppedTweens.Add(tween);
                continue;
            }

            tween.Update(deltaTime);
        }
    }
}
