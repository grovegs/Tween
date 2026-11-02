using System.Threading;

namespace GroveGames.Tween;

internal static class TweenStorageIds
{
    private static int s_last = -1;

    public static int Next()
    {
        return Interlocked.Increment(ref s_last);
    }
}
