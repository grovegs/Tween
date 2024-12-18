using GroveGames.Tween.Core;

namespace GroveGames.Tween.Pooling;

public class TweenPool
{
    private readonly Dictionary<Type, Stack<ITween>> _tweenPool;
    private readonly Stack<ISequence> _sequencePool;

    public TweenPool()
    {
        _tweenPool = [];
        _sequencePool = [];
    }

    public ISequence GetSequence()
    {
        if (_sequencePool.Count > 0)
        {
            var sequence = _sequencePool.Pop();
            sequence.SetOnStop(() => ReturnSequence(sequence));
            return sequence;
        }

        var newSequence = new Sequence();
        newSequence.SetOnStop(() => ReturnSequence(newSequence));
        return newSequence;
    }

    public ITween GetTween<T>()
    {
        if (_tweenPool.TryGetValue(typeof(T), out var stack))
        {
            if (stack.Count > 0)
            {
                var pooledTween = stack.Pop();
                pooledTween.SetOnStop(() => ReturnTween<T>(pooledTween));
                return pooledTween;
            }

            var newTween = new Tween<T>();
            newTween.SetOnStop(() => ReturnTween<T>(newTween));

            return newTween;
        }

        var newStack = new Stack<ITween>();
        _tweenPool[typeof(T)] = newStack;
        var tween = new Tween<T>();
        tween.SetOnStop(() => ReturnTween<T>(tween));
        return tween;
    }

    private void ReturnTween<T>(ITween tween)
    {
        tween.Reset();
        _tweenPool[typeof(T)].Push(tween);
    }

    private void ReturnSequence(ISequence sequence)
    {
        sequence.Reset();
        _sequencePool.Push(sequence);
    }
}
