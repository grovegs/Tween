namespace GroveGames.Tween;

internal static class TweenStorageId<T, TAdapter> where TAdapter : struct, ITweenAdapter<T>
{
    public static readonly int Value = TweenStorageIds.Next();
}
