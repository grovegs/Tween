using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Constraints;
using Is = UnityEngine.TestTools.Constraints.Is;

namespace GroveGames.Tween.Unity.Editor.Tests
{
    public sealed class TweenerExtensionsTests
    {
        private const float Tolerance = 0.0001f;

        private static readonly Vector3[] s_points = { new(1f, 0f, 0f), new(0f, 1f, 0f) };
        private static readonly Action<Vector3, TweenTarget> s_setPosition = SetPosition;

        [Test]
        public void Create_Vector2_InterpolatesValue()
        {
            var tweener = new Tweener(Debug.LogException);
            var value = Vector2.zero;
            tweener.Create(Vector2.zero, new Vector2(2f, 4f), 1f).Bind(new TweenTarget(), (current, _) => value = current);

            Step(tweener, 0.5f);

            Assert.That(Vector2.Distance(value, new Vector2(1f, 2f)), Is.LessThan(Tolerance));
        }

        [Test]
        public void Create_Vector3ThroughInterface_InterpolatesValue()
        {
            ITweener tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            tweener.Create(Vector3.zero, new Vector3(2f, 4f, 6f), 1f).Bind(target, s_setPosition);

            Step((Tweener)tweener, 0.5f);

            Assert.That(Vector3.Distance(target.Position, new Vector3(1f, 2f, 3f)), Is.LessThan(Tolerance));
        }

        [Test]
        public void Create_Color_InterpolatesValue()
        {
            var tweener = new Tweener(Debug.LogException);
            var value = Color.clear;
            tweener.Create(Color.clear, Color.white, 1f).Bind(new TweenTarget(), (current, _) => value = current);

            Step(tweener, 0.5f);

            Assert.That(value.a, Is.EqualTo(0.5f).Within(Tolerance));
        }

        [Test]
        public void Create_Vector3WithSettings_UsesSettings()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            var settings = new TweenSettings(1f, loops: 2, loopType: LoopType.Yoyo);
            tweener.Create(Vector3.zero, Vector3.one, in settings).Bind(target, s_setPosition);

            Step(tweener, 1.5f);

            Assert.That(Vector3.Distance(target.Position, Vector3.one * 0.5f), Is.LessThan(Tolerance));
        }

        [Test]
        public void Update_Punch_ReturnsToStartAtEnd()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            tweener.CreatePunch(Vector3.one, Vector3.one, 1f).Bind(target, s_setPosition);

            Step(tweener, 0.05f);
            var moved = target.Position;
            Step(tweener, 1f);

            Assert.That(moved, Is.Not.EqualTo(Vector3.one));
            Assert.That(target.Position, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Update_Shake_PassesThroughPointsInOrder()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            tweener.CreateShake(Vector3.zero, s_points, 3f).Bind(target, s_setPosition);

            Step(tweener, 1f);
            var first = target.Position;
            Step(tweener, 0.5f);
            var between = target.Position;
            Step(tweener, 0.5f);
            var second = target.Position;

            Assert.That(Vector3.Distance(first, s_points[0]), Is.LessThan(Tolerance));
            Assert.That(Vector3.Distance(between, new Vector3(0.5f, 0.5f, 0f)), Is.LessThan(Tolerance));
            Assert.That(Vector3.Distance(second, s_points[1]), Is.LessThan(Tolerance));
        }

        [Test]
        public void Update_Shake_ReturnsToStartAtEnd()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            tweener.CreateShake(Vector3.one, s_points, 1f).Bind(target, s_setPosition);

            Step(tweener, 2f);

            Assert.That(target.Position, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Update_ShakeWithoutPoints_StaysAtStart()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            tweener.CreateShake(Vector3.one, Array.Empty<Vector3>(), 1f).Bind(target, s_setPosition);

            Step(tweener, 0.5f);

            Assert.That(target.Position, Is.EqualTo(Vector3.one));
        }

        [Test]
        public void Shake_ReusedPoints_DoesNotAllocate()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            RunShake(tweener, target);

            Assert.That(() => RunShake(tweener, target), Is.Not.AllocatingGCMemory());
        }

        private static void RunShake(Tweener tweener, TweenTarget target)
        {
            tweener.CreateShake(Vector3.zero, s_points, 0.1f).Bind(target, s_setPosition);

            for (var i = 0; i < 10; i++)
            {
                Step(tweener, 0.016f);
            }
        }

        private static void Step(Tweener tweener, float deltaTime)
        {
            tweener.Update(deltaTime, deltaTime, deltaTime);
        }

        private static void SetPosition(Vector3 position, TweenTarget target)
        {
            target.SetPosition(position);
        }
    }
}
