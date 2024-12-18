using GroveGames.Tween.Core;

namespace GroveGames.Tween.Pooling;

public class TweenPool
{
    private readonly Dictionary<Type, Stack<ITween>> _tweenPool;

    public TweenPool()
    {
        _tweenPool = [];
    }

    public ITween GetTween<T>()
    {
        if (_tweenPool.TryGetValue(typeof(T), out var stack))
        {
            if (stack.Count > 0)
            {
                var pooledTween = stack.Pop();
                pooledTween.SetOnStop(() => Return<T>(pooledTween));
                return pooledTween;
            }

            var newTween = new Tween<T>();
            newTween.SetOnStop(() => Return<T>(newTween));

            return newTween;
        }

        var newStack = new Stack<ITween>();
        _tweenPool[typeof(T)] = newStack;
        var tween = new Tween<T>();
        tween.SetOnStop(() => Return<T>(tween));
        return tween;
    }

    private void Return<T>(ITween tween)
    {
        tween.Reset();
        _tweenPool[typeof(T)].Push(tween);
    }
}
