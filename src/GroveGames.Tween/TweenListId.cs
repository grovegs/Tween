using System.Threading;

namespace GroveGames.Tween;

internal static class TweenListId
{
    private static int s_last = -1;

    public static int Next()
    {
        return Interlocked.Increment(ref s_last);
    }
}

internal static class TweenListId<T, TAdapter> where TAdapter : struct, ITweenAdapter<T>
{
    public static readonly int Value = TweenListId.Next();
}
