using GroveGames.ObjectPool;
using GroveGames.ObjectPool.Pools;
using GroveGames.Tween.Core;
using GroveGames.Tween.Pooling;

namespace GroveGames.Tween.Context;

public class TweenerContext
{
    private readonly List<ITween> _tweens;
    private readonly HashSet<ITween> _stoppedTweens;

    private readonly IMultiTypeObjectPool<ITween> _multiTypeObjectPool;
    private readonly IObjectPool<ISequence> _sequencePool;

    public TweenerContext()
    {
        _tweens = [];
        _stoppedTweens = [];
        _multiTypeObjectPool = new MultiTypeObjectPool<ITween>();
        _sequencePool = new ObjectPool<ISequence>(10, new SequencePooledObjectStrategy());
    }

    public void AddPoolStrategy<T>(IPooledObjectStrategy<ITween> strategy) where T : class, ITween
    {
        _multiTypeObjectPool.AddPooledObjectStrategy<T>(strategy);
    }

    public ITween<T> CreateTween<T>(Func<T> start, Func<T> end, float duration, Func<T, T, float, T> lerpFunc, bool autoPlay)
    {
        var tween = _multiTypeObjectPool.Get<ITween<T>>();
        var tweenInstance = (Tween<T>)tween;
        tweenInstance.Construct(start, end, duration, lerpFunc, autoPlay);
        tween.SetOnStop(() => _multiTypeObjectPool.Return(tween));
        tween.SetOnComplete(() => _stoppedTweens.Add(tween));
        _tweens.Add(tween);

        return tweenInstance;
    }

    public ISequence CreateSequnce()
    {
        var sequence = _sequencePool.Get();
        sequence.SetOnComplete(() => _stoppedTweens.Add(sequence));
        sequence.SetOnStop(() => _sequencePool.Return(sequence));
        _tweens.Add(sequence);

        return sequence;
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
