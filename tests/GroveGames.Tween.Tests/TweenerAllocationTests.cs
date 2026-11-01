using System;
using System.Numerics;

namespace GroveGames.Tween.Tests;

public sealed class TweenerAllocationTests
{
    private readonly struct TestVector3Adapter : ITweenAdapter<Vector3>
    {
        public Vector3 Evaluate(in Vector3 from, in Vector3 to, float progress)
        {
            return Vector3.Lerp(from, to, progress);
        }
    }

    private sealed class TestTarget
    {
        private float _value;
        private Vector3 _position;

        public float Value => _value;

        public Vector3 Position => _position;

        public TestTarget()
        {
            _value = 0f;
            _position = Vector3.Zero;
        }

        public void SetValue(float value)
        {
            _value = value;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
        }
    }

    private const int TweenCount = 1000;

    private static readonly Action<float, TestTarget> s_setValue = SetValue;
    private static readonly Action<Vector3, TestTarget> s_setPosition = SetPosition;
    private static readonly Action s_callback = static () => { };
    private static readonly Vector3[] s_points = { new(1f, 0f, 0f), new(0f, 1f, 0f) };

    [Fact]
    public void CreateUpdateAndCancel_AfterWarmUp_DoesNotAllocate()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var handles = new TweenHandle[TweenCount];
        Run(tweener, target, handles);

        var allocated = Measure(() => Run(tweener, target, handles));

        Assert.Equal(0, allocated);
    }

    [Fact]
    public void Sequence_AfterWarmUp_DoesNotAllocate()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        RunSequence(tweener, target);

        var allocated = Measure(() => RunSequence(tweener, target));

        Assert.Equal(0, allocated);
    }

    [Fact]
    public void Bind_StaticLambdaSetter_DoesNotAllocate()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        BindStaticLambda(tweener, target);

        var allocated = Measure(() => BindStaticLambda(tweener, target));

        Assert.Equal(0, allocated);
    }

    [Fact]
    public void Shake_ReusedPoints_DoesNotAllocate()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        RunShake(tweener, target);

        var allocated = Measure(() => RunShake(tweener, target));

        Assert.Equal(0, allocated);
    }

    private static long Measure(Action action)
    {
        var before = GC.GetAllocatedBytesForCurrentThread();
        action();
        return GC.GetAllocatedBytesForCurrentThread() - before;
    }

    private static void BindStaticLambda(Tweener tweener, TestTarget target)
    {
        var handle = tweener.Create(0f, 1f, 1f).Bind(target, static (value, state) => state.SetValue(value));
        tweener.Update(0.016f, 0.016f, 0.016f);
        handle.Cancel();
        tweener.Update(0f, 0f, 0f);
    }

    private static void Run(Tweener tweener, TestTarget target, TweenHandle[] handles)
    {
        for (var i = 0; i < handles.Length; i++)
        {
            handles[i] = i % 2 == 0
                ? tweener.Create(0f, 1f, 1f).WithEase(Ease.OutQuad).Bind(target, s_setValue)
                : tweener.Create<Vector3, TestVector3Adapter>(Vector3.Zero, Vector3.One, 1f).WithLoops(2, LoopType.Yoyo).Bind(target, s_setPosition);
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

    private static void RunSequence(Tweener tweener, TestTarget target)
    {
        tweener.CreateSequence()
            .Append(tweener.Create(0f, 1f, 0.1f), target, s_setValue)
            .AppendInterval(0.1f)
            .Join(tweener.Create<Vector3, TestVector3Adapter>(Vector3.Zero, Vector3.One, 0.1f), target, s_setPosition)
            .AppendCallback(s_callback);

        for (var i = 0; i < 30; i++)
        {
            tweener.Update(0.016f, 0.016f, 0.016f);
        }
    }

    private static void RunShake(Tweener tweener, TestTarget target)
    {
        tweener.CreateShake<Vector3, TestVector3Adapter>(Vector3.Zero, s_points, 0.1f).Bind(target, s_setPosition);

        for (var i = 0; i < 10; i++)
        {
            tweener.Update(0.016f, 0.016f, 0.016f);
        }
    }

    private static Tweener CreateTweener()
    {
        return new Tweener(static exception => throw new InvalidOperationException("Unexpected tween exception", exception));
    }

    private static void SetValue(float value, TestTarget target)
    {
        target.SetValue(value);
    }

    private static void SetPosition(Vector3 position, TestTarget target)
    {
        target.SetPosition(position);
    }
}
