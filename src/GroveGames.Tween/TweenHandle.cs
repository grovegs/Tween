using System;

namespace GroveGames.Tween;

public readonly struct TweenHandle : IEquatable<TweenHandle>
{
    private readonly Tweener? _tweener;
    private readonly int _listId;
    private readonly int _slot;
    private readonly int _version;

    internal TweenHandle(Tweener tweener, int listId, int slot, int version)
    {
        _tweener = tweener;
        _listId = listId;
        _slot = slot;
        _version = version;
    }

    public bool IsActive => _tweener != null && _tweener.IsActive(_listId, _slot, _version);

    public bool Cancel()
    {
        return _tweener != null && _tweener.Cancel(_listId, _slot, _version);
    }

    public bool Equals(TweenHandle other)
    {
        return ReferenceEquals(_tweener, other._tweener) && _listId == other._listId && _slot == other._slot && _version == other._version;
    }

    public override bool Equals(object? obj)
    {
        return obj is TweenHandle other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_listId, _slot, _version);
    }
}
