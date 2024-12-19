using System;

using GroveGames.ObjectPool;
using GroveGames.Tween.Core;

namespace GroveGames.Tween.Pooling;

public class FloatTweenPooledObjectStrategy : IPooledObjectStrategy<ITween>
{
    public ITween Create()
    {
        return new Tween<float>();
    }

    public void Get(ITween pooledObject)
    {

    }

    public void Return(ITween pooledObject)
    {
        pooledObject.Reset();
    }
}
