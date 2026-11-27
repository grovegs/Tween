namespace GroveGames.Tween;

internal static class TweenListId<T, TAdapter> where TAdapter : struct, ITweenAdapter<T>
{
    public static readonly int Value = TweenListIds.Next();
}
