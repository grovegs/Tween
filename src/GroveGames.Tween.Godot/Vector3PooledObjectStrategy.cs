using System;


using GroveGames.ObjectPool;
using GroveGames.Tween.Core;

namespace GroveGames.Tween;

public class Vector3PooledObjectStrategy : IPooledObjectStrategy<ITween>
{
    public ITween Create()
    {
        return new Tween<Godot.Vector3>();
    }

    public void Get(ITween pooledObject)
    {

    }

    public void Return(ITween pooledObject)
    {
        pooledObject.Reset();
    }
}
