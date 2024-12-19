using GroveGames.ObjectPool;
using GroveGames.Tween.Core;

namespace GroveGames.Tween.Pooling;

public class IntTweenPooledObjectStrategy : IPooledObjectStrategy<ITween>
{
    public ITween Create()
    {
        return new Tween<int>();
    }

    public void Get(ITween pooledObject)
    {

    }

    public void Return(ITween pooledObject)
    {
        pooledObject.Reset();
    }
}
