using System.Collections.Generic;
using UnityEngine;

namespace GroveGames.Tween.Unity
{
    public static class TweenerExtensions
    {
        public static TweenBuilder<Vector2> Create(this ITweener tweener, Vector2 from, Vector2 to, float duration)
        {
            return tweener is Tweener concrete
                ? concrete.Create<Vector2, Vector2TweenAdapter>(from, to, duration)
                : tweener.Create<Vector2, Vector2TweenAdapter>(from, to, duration);
        }

        public static TweenBuilder<Vector2> Create(this ITweener tweener, Vector2 from, Vector2 to, in TweenSettings settings)
        {
            return tweener is Tweener concrete
                ? concrete.Create<Vector2, Vector2TweenAdapter>(from, to, in settings)
                : tweener.Create<Vector2, Vector2TweenAdapter>(from, to, in settings);
        }

        public static TweenBuilder<Vector3> Create(this ITweener tweener, Vector3 from, Vector3 to, float duration)
        {
            return tweener is Tweener concrete
                ? concrete.Create<Vector3, Vector3TweenAdapter>(from, to, duration)
                : tweener.Create<Vector3, Vector3TweenAdapter>(from, to, duration);
        }

        public static TweenBuilder<Vector3> Create(this ITweener tweener, Vector3 from, Vector3 to, in TweenSettings settings)
        {
            return tweener is Tweener concrete
                ? concrete.Create<Vector3, Vector3TweenAdapter>(from, to, in settings)
                : tweener.Create<Vector3, Vector3TweenAdapter>(from, to, in settings);
        }

        public static TweenBuilder<Color> Create(this ITweener tweener, Color from, Color to, float duration)
        {
            return tweener is Tweener concrete
                ? concrete.Create<Color, ColorTweenAdapter>(from, to, duration)
                : tweener.Create<Color, ColorTweenAdapter>(from, to, duration);
        }

        public static TweenBuilder<Color> Create(this ITweener tweener, Color from, Color to, in TweenSettings settings)
        {
            return tweener is Tweener concrete
                ? concrete.Create<Color, ColorTweenAdapter>(from, to, in settings)
                : tweener.Create<Color, ColorTweenAdapter>(from, to, in settings);
        }

        public static TweenBuilder<Vector3> CreatePunch(this ITweener tweener, Vector3 from, Vector3 strength, float duration, int frequency = 10, float dampingRatio = 1f)
        {
            return tweener is Tweener concrete
                ? concrete.CreatePunch<Vector3, Vector3TweenAdapter>(from, from + strength, duration, frequency, dampingRatio)
                : tweener.CreatePunch<Vector3, Vector3TweenAdapter>(from, from + strength, duration, frequency, dampingRatio);
        }

        public static TweenBuilder<Vector2> CreateShake(this ITweener tweener, Vector2 from, IReadOnlyList<Vector2> points, float duration)
        {
            return tweener is Tweener concrete
                ? concrete.CreateShake<Vector2, Vector2TweenAdapter>(from, points, duration)
                : tweener.CreateShake<Vector2, Vector2TweenAdapter>(from, points, duration);
        }

        public static TweenBuilder<Vector3> CreateShake(this ITweener tweener, Vector3 from, IReadOnlyList<Vector3> points, float duration)
        {
            return tweener is Tweener concrete
                ? concrete.CreateShake<Vector3, Vector3TweenAdapter>(from, points, duration)
                : tweener.CreateShake<Vector3, Vector3TweenAdapter>(from, points, duration);
        }
    }
}
