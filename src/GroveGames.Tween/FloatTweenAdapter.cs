using System.Runtime.CompilerServices;

namespace GroveGames.Tween;

internal readonly struct FloatTweenAdapter : ITweenAdapter<float>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Evaluate(in float from, in float to, float progress)
    {
        return from + (to - from) * progress;
    }
}
