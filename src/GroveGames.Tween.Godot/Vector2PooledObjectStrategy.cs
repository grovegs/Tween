using GroveGames.ObjectPool;
using GroveGames.Tween.Core;

namespace GroveGames.Tween;

public class Vector2PooledObjectStrategy : IPooledObjectStrategy<ITween>
{
    public ITween Create()
    {
        return new Tween<Godot.Vector2>();
    }

    public void Get(ITween pooledObject)
    {

    }

    public void Return(ITween pooledObject)
    {
        pooledObject.Reset();
    }
}
