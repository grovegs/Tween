using System.Runtime.CompilerServices;
using UnityEngine;

namespace GroveGames.Tween.Unity
{
    internal readonly struct Vector3TweenAdapter : ITweenAdapter<Vector3>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 Evaluate(in Vector3 from, in Vector3 to, float progress)
        {
            return Vector3.LerpUnclamped(from, to, progress);
        }
    }
}
