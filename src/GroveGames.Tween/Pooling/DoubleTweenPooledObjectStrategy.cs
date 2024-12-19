using GroveGames.ObjectPool;
using GroveGames.Tween.Core;

namespace GroveGames.Tween.Pooling;

public class DoubleTweenPooledObjectStrategy : IPooledObjectStrategy<ITween>
{
    public ITween Create()
    {
        return new Tween<double>();
    }

    public void Get(ITween pooledObject)
    {

    }

    public void Return(ITween pooledObject)
    {
        pooledObject.Reset();
    }
}
