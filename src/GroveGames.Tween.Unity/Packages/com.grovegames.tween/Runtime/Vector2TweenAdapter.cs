using System.Runtime.CompilerServices;
using UnityEngine;

namespace GroveGames.Tween.Unity
{
    internal readonly struct Vector2TweenAdapter : ITweenAdapter<Vector2>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2 Evaluate(in Vector2 from, in Vector2 to, float progress)
        {
            return Vector2.LerpUnclamped(from, to, progress);
        }
    }
}
