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
        tween.SetOnStop(OnCompleteOrStop);
        tween.SetOnComplete(OnCompleteOrStop);
        _tweens.Add(tween);

        return tweenInstance;

        void OnCompleteOrStop()
        {
            _stoppedTweens.Add(tween);
            _multiTypeObjectPool.Return(tween);
        }
    }

    public ISequence CreateSequence(bool autoPlay = true)
    {
        var sequence = _sequencePool.Get();
        ((Sequence)sequence).Construct(autoPlay);
        sequence.SetOnComplete(OnCompleteOrStop);
        sequence.SetOnStop(OnCompleteOrStop);
        _tweens.Add(sequence);

        return sequence;

        void OnCompleteOrStop()
        {
            _stoppedTweens.Add(sequence);
            _sequencePool.Return(sequence);
        }
    }

    public void Stop(int id)
    {
        foreach (var tween in _tweens)
        {
            if (tween.Id == id)
            {
                tween.Stop();
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
                _tweens.RemoveAt(i);
                _stoppedTweens.Remove(tween);
                continue;
            }

            tween.Update(deltaTime);
        }
    }
}
