using System;
using System.Collections.Generic;

namespace GroveGames.Tween.Tests;

public sealed class TweenerSafetyTests
{
    private sealed class TestTarget
    {
        private float _value;

        public float Value => _value;

        public TestTarget()
        {
            _value = 0f;
        }

        public void SetValue(float value)
        {
            _value = value;
        }
    }

    private const float Tolerance = 0.0001f;

    [Fact]
    public void Update_SetterThrows_ReportsAndRemovesOnlyThatTween()
    {
        var exceptions = new List<Exception>();
        var tweener = new Tweener(exceptions.Add);
        var before = new TestTarget();
        var after = new TestTarget();
        tweener.Create(0f, 1f, 1f).Bind(before, static (value, target) => target.SetValue(value));
        var failing = tweener.Create(0f, 1f, 1f).Bind(new TestTarget(), static (value, _) =>
        {
            if (value > 0f)
            {
                throw new InvalidOperationException("Boom");
            }
        });
        tweener.Create(0f, 1f, 1f).Bind(after, static (value, target) => target.SetValue(value));

        Step(tweener, 0.25f);
        Step(tweener, 0.25f);

        Assert.Equal("Boom", Assert.Single(exceptions).Message);
        Assert.False(failing.IsActive);
        Assert.Equal(0.5f, before.Value, Tolerance);
        Assert.Equal(0.5f, after.Value, Tolerance);
    }

    [Fact]
    public void Update_CompletionCallbackThrows_ReportsAndKeepsOtherTweens()
    {
        var exceptions = new List<Exception>();
        var tweener = new Tweener(exceptions.Add);
        var other = new TestTarget();
        tweener.Create(0f, 1f, 0.5f).WithCompletionCallback(static () => throw new InvalidOperationException("Boom")).Bind(new TestTarget(), static (value, target) => target.SetValue(value));
        tweener.Create(0f, 1f, 1f).Bind(other, static (value, target) => target.SetValue(value));

        Step(tweener, 0.5f);
        Step(tweener, 0.25f);

        Assert.Equal("Boom", Assert.Single(exceptions).Message);
        Assert.Equal(0.75f, other.Value, Tolerance);
    }

    [Fact]
    public void Update_SequenceCallbackThrows_ReportsAndKeepsSequenceRunning()
    {
        var exceptions = new List<Exception>();
        var tweener = new Tweener(exceptions.Add);
        var target = new TestTarget();
        tweener.CreateSequence()
            .AppendCallback(static () => throw new InvalidOperationException("Boom"))
            .Append(tweener.Create(0f, 1f, 1f), target, static (value, state) => state.SetValue(value));

        Step(tweener, 0.5f);

        Assert.Equal("Boom", Assert.Single(exceptions).Message);
        Assert.Equal(0.5f, target.Value, Tolerance);
    }

    [Fact]
    public void Handle_RemoveFromList_RemovesMatchingHandle()
    {
        var tweener = new Tweener(static exception => throw new InvalidOperationException("Unexpected tween exception", exception));
        var first = tweener.Create(0f, 1f, 1f).Bind(new TestTarget(), static (value, target) => target.SetValue(value));
        var second = tweener.Create(0f, 1f, 1f).Bind(new TestTarget(), static (value, target) => target.SetValue(value));
        var handles = new List<TweenHandle> { first, second };

        var isRemoved = handles.Remove(second);

        Assert.True(isRemoved);
        Assert.Equal(new[] { first }, handles);
    }

    private static void Step(Tweener tweener, float deltaTime)
    {
        tweener.Update(deltaTime, deltaTime, deltaTime);
    }
}
