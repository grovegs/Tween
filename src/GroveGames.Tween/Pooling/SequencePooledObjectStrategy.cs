using System;

using GroveGames.ObjectPool;
using GroveGames.Tween.Core;

namespace GroveGames.Tween.Pooling;

public class SequencePooledObjectStrategy : IPooledObjectStrategy<ISequence>
{
    public ISequence Create()
    {
        return new Sequence();
    }

    public void Get(ISequence pooledObject)
    {

    }

    public void Return(ISequence pooledObject)
    {
        pooledObject.Reset();
    }
}
