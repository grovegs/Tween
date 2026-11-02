using System.Runtime.CompilerServices;
using UnityEngine;

namespace GroveGames.Tween.Unity
{
    internal readonly struct ColorTweenAdapter : ITweenAdapter<Color>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color Evaluate(in Color from, in Color to, float progress)
        {
            return Color.LerpUnclamped(from, to, progress);
        }
    }
}
