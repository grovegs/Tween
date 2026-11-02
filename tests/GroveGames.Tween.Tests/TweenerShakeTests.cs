using System;
using System.Numerics;

namespace GroveGames.Tween.Tests;

public sealed class TweenerShakeTests
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

    private const float Tolerance = 0.0001f;

    private static readonly Vector3[] s_points = { new(1f, 0f, 0f), new(0f, 1f, 0f) };

    [Fact]
    public void Update_Shake_PassesThroughPointsInOrder()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreateShake<Vector3, TestVector3Adapter>(Vector3.Zero, s_points, 3f).Bind(target, static (position, state) => state.SetPosition(position));

        Step(tweener, 1f);
        var first = target.Position;
        Step(tweener, 0.5f);
        var between = target.Position;
        Step(tweener, 0.5f);
        var second = target.Position;

        Assert.True(Vector3.Distance(first, s_points[0]) < Tolerance);
        Assert.True(Vector3.Distance(between, new Vector3(0.5f, 0.5f, 0f)) < Tolerance);
        Assert.True(Vector3.Distance(second, s_points[1]) < Tolerance);
    }

    [Fact]
    public void Update_Shake_ReturnsToStartAtEnd()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreateShake<Vector3, TestVector3Adapter>(Vector3.One, s_points, 1f).Bind(target, static (position, state) => state.SetPosition(position));

        Step(tweener, 2f);

        Assert.Equal(Vector3.One, target.Position);
    }

    [Fact]
    public void Update_ShakeWithoutPoints_StaysAtStart()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreateShake<Vector3, TestVector3Adapter>(Vector3.One, Array.Empty<Vector3>(), 1f).Bind(target, static (position, state) => state.SetPosition(position));

        Step(tweener, 0.5f);

        Assert.Equal(Vector3.One, target.Position);
    }

    [Fact]
    public void Update_FloatShake_PassesThroughPoints()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreateShake(0f, new[] { 2f, -2f }, 3f).Bind(target, static (value, state) => state.SetValue(value));

        Step(tweener, 1.5f);

        Assert.Equal(0f, target.Value, Tolerance);
    }

    private static Tweener CreateTweener()
    {
        return new Tweener(static exception => throw new InvalidOperationException("Unexpected tween exception", exception));
    }

    private static void Step(Tweener tweener, float deltaTime)
    {
        tweener.Update(deltaTime, deltaTime, deltaTime);
    }
}
