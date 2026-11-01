using System;

namespace GroveGames.Tween;

public readonly struct TweenSequenceHandle : IEquatable<TweenSequenceHandle>
{
    private readonly Tweener? _tweener;
    private readonly int _slot;
    private readonly int _version;

    internal TweenSequenceHandle(Tweener tweener, int slot, int version)
    {
        _tweener = tweener;
        _slot = slot;
        _version = version;
    }

    public bool IsActive => _tweener != null && _tweener.IsSequenceActive(_slot, _version);

    public bool Cancel()
    {
        return _tweener != null && _tweener.CancelSequence(_slot, _version);
    }

    public bool Equals(TweenSequenceHandle other)
    {
        return ReferenceEquals(_tweener, other._tweener) && _slot == other._slot && _version == other._version;
    }

    public override bool Equals(object? obj)
    {
        return obj is TweenSequenceHandle other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_slot, _version);
    }
}
