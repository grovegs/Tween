using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Constraints;
using Is = UnityEngine.TestTools.Constraints.Is;

namespace GroveGames.Tween.Unity.Editor.Tests
{
    public sealed class TweenerAllocationTests
    {
        private const int TweenCount = 1000;

        private static readonly Action<float, TweenTarget> s_setValue = SetValue;
        private static readonly Action<Vector3, TweenTarget> s_setPosition = SetPosition;
        private static readonly Action s_callback = () => { };

        [Test]
        public void CreateUpdateAndCancel_AfterWarmUp_DoesNotAllocate()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            var handles = new TweenHandle[TweenCount];
            Run(tweener, target, handles);

            Assert.That(() => Run(tweener, target, handles), Is.Not.AllocatingGCMemory());
        }

        [Test]
        public void Sequence_AfterWarmUp_DoesNotAllocate()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            RunSequence(tweener, target);

            Assert.That(() => RunSequence(tweener, target), Is.Not.AllocatingGCMemory());
        }

        [Test]
        public void Bind_StaticLambdaSetter_DoesNotAllocate()
        {
            var tweener = new Tweener(Debug.LogException);
            var target = new TweenTarget();
            BindStaticLambda(tweener, target);

            Assert.That(() => BindStaticLambda(tweener, target), Is.Not.AllocatingGCMemory());
        }

        private static void BindStaticLambda(Tweener tweener, TweenTarget target)
        {
            var handle = tweener.Create(0f, 1f, 1f).Bind(target, static (value, state) => state.SetValue(value));
            tweener.Update(0.016f, 0.016f, 0.016f);
            handle.Cancel();
            tweener.Update(0f, 0f, 0f);
        }

        private static void Run(Tweener tweener, TweenTarget target, TweenHandle[] handles)
        {
            for (var i = 0; i < handles.Length; i++)
            {
                handles[i] = i % 2 == 0
                    ? tweener.Create(0f, 1f, 1f).WithEase(Ease.OutQuad).Bind(target, s_setValue)
                    : tweener.Create(Vector3.zero, Vector3.one, 1f).WithLoops(2, LoopType.Yoyo).Bind(target, s_setPosition);
            }

            for (var i = 0; i < 10; i++)
            {
                tweener.Update(0.016f, 0.016f, 0.016f);
            }

            for (var i = 0; i < handles.Length; i++)
            {
                handles[i].Cancel();
            }

            tweener.Update(0f, 0f, 0f);
        }

        private static void RunSequence(Tweener tweener, TweenTarget target)
        {
            tweener.CreateSequence()
                .Append(tweener.Create(0f, 1f, 0.1f), target, s_setValue)
                .AppendInterval(0.1f)
                .Join(tweener.Create(Vector3.zero, Vector3.one, 0.1f), target, s_setPosition)
                .AppendCallback(s_callback);

            for (var i = 0; i < 30; i++)
            {
                tweener.Update(0.016f, 0.016f, 0.016f);
            }
        }

        private static void SetValue(float value, TweenTarget target)
        {
            target.SetValue(value);
        }

        private static void SetPosition(Vector3 position, TweenTarget target)
        {
            target.SetPosition(position);
        }
    }
}
