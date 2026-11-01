using System;
using GroveGames.Scheduling;

namespace GroveGames.Tween.Tests;

public sealed class TweenSequenceTests
{
    private sealed class TestTarget
    {
        private float _value;
        private int _setCount;
        private int _completedCount;

        public float Value => _value;

        public int SetCount => _setCount;

        public int CompletedCount => _completedCount;

        public TestTarget()
        {
            _value = 0f;
            _setCount = 0;
            _completedCount = 0;
        }

        public void SetValue(float value)
        {
            _value = value;
            _setCount++;
        }

        public void Complete()
        {
            _completedCount++;
        }
    }

    private const float Tolerance = 0.0001f;

    private static readonly Action<float, TestTarget> s_setValue = SetValue;

    [Fact]
    public void Append_TwoTweens_PlaysInOrder()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreateSequence()
            .Append(tweener.Create(0f, 1f, 1f), target, s_setValue)
            .Append(tweener.Create(1f, 3f, 1f), target, s_setValue);

        Step(tweener, 0.5f);
        var first = target.Value;
        Step(tweener, 1f);

        Assert.Equal(0.5f, first, Tolerance);
        Assert.Equal(2f, target.Value, Tolerance);
    }

    [Fact]
    public void Append_SamePropertyAtBoundary_LaterTweenWins()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreateSequence()
            .Append(tweener.Create(0f, 1f, 1f), target, s_setValue)
            .Append(tweener.Create(5f, 6f, 1f), target, s_setValue);

        Step(tweener, 1.25f);

        Assert.Equal(5.25f, target.Value, Tolerance);
    }

    [Fact]
    public void Append_StatementForm_KeepsCursor()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var sequence = tweener.CreateSequence();

        for (var i = 0; i < 3; i++)
        {
            sequence.Append(tweener.Create(i, i + 1f, 1f), target, s_setValue);
        }

        Step(tweener, 2.5f);

        Assert.Equal(2.5f, target.Value, Tolerance);
    }

    [Fact]
    public void Join_StartsWithPreviousTween()
    {
        var tweener = CreateTweener();
        var first = new TestTarget();
        var second = new TestTarget();
        tweener.CreateSequence()
            .AppendInterval(1f)
            .Append(tweener.Create(0f, 1f, 1f), first, s_setValue)
            .Join(tweener.Create(0f, 1f, 2f), second, s_setValue);

        Step(tweener, 1.5f);

        Assert.Equal(0.5f, first.Value, Tolerance);
        Assert.Equal(0.25f, second.Value, Tolerance);
    }

    [Fact]
    public void Append_TweenWithDelay_StartsAfterDelay()
    {
        var tweener = CreateTweener();
        var first = new TestTarget();
        var second = new TestTarget();
        tweener.CreateSequence()
            .Append(tweener.Create(0f, 1f, 1f).WithDelay(0.5f), first, s_setValue)
            .Append(tweener.Create(0f, 1f, 1f), second, s_setValue);

        Step(tweener, 1.75f);

        Assert.Equal(1f, first.Value);
        Assert.Equal(0.25f, second.Value, Tolerance);
    }

    [Fact]
    public void Append_TweenWithLoopInterval_StartsNextAfterAllLoops()
    {
        var tweener = CreateTweener();
        var second = new TestTarget();
        tweener.CreateSequence()
            .Append(tweener.Create(0f, 1f, 1f).WithLoops(2, LoopType.Yoyo, 1f), new TestTarget(), s_setValue)
            .Append(tweener.Create(0f, 1f, 1f), second, s_setValue);

        Step(tweener, 3.5f);

        Assert.Equal(0.5f, second.Value, Tolerance);
    }

    [Fact]
    public void AppendCallback_InvokesAtCursor()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreateSequence()
            .Append(tweener.Create(0f, 1f, 1f), target, s_setValue)
            .AppendCallback(target.Complete);

        Step(tweener, 0.75f);
        var beforeEnd = target.CompletedCount;
        Step(tweener, 0.25f);

        Assert.Equal(0, beforeEnd);
        Assert.Equal(1, target.CompletedCount);
    }

    [Fact]
    public void Handle_AllChildrenCompleted_IsNotActive()
    {
        var tweener = CreateTweener();
        var handle = tweener.CreateSequence()
            .Append(tweener.Create(0f, 1f, 1f), new TestTarget(), s_setValue)
            .AppendCallback(() => { })
            .Handle;

        var activeBefore = handle.IsActive;
        Step(tweener, 1f);

        Assert.True(activeBefore);
        Assert.False(handle.IsActive);
    }

    [Fact]
    public void Handle_EmptySequence_IsNotActive()
    {
        var tweener = CreateTweener();

        var handle = tweener.CreateSequence().Handle;
        Step(tweener, 0f);

        Assert.False(handle.IsActive);
    }

    [Fact]
    public void Cancel_StopsTweensAndCallbacks()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var handle = tweener.CreateSequence()
            .Append(tweener.Create(0f, 1f, 1f), target, s_setValue)
            .AppendCallback(target.Complete)
            .Handle;

        Step(tweener, 0.5f);
        var isCanceled = handle.Cancel();
        Step(tweener, 2f);

        Assert.True(isCanceled);
        Assert.False(handle.IsActive);
        Assert.Equal(0.5f, target.Value, Tolerance);
        Assert.Equal(0, target.CompletedCount);
    }

    [Fact]
    public void Token_Canceled_StopsSequence()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var token = new ScheduleToken();
        var handle = tweener.CreateSequence(token: token)
            .Append(tweener.Create(0f, 1f, 1f), target, s_setValue)
            .AppendCallback(target.Complete)
            .Handle;

        token.Cancel();
        Step(tweener, 2f);

        Assert.False(handle.IsActive);
        Assert.Equal(0, target.CompletedCount);
    }

    [Fact]
    public void TimeMode_Unscaled_OverridesChildTimeMode()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreateSequence(TimeMode.Unscaled).Append(tweener.Create(0f, 1f, 1f), target, s_setValue);

        tweener.Update(0f, 0.5f, 0f);

        Assert.Equal(0.5f, target.Value, Tolerance);
    }

    private static void Step(Tweener tweener, float deltaTime)
    {
        tweener.Update(deltaTime, deltaTime, deltaTime);
    }

    private static void SetValue(float value, TestTarget target)
    {
        target.SetValue(value);
    }

    private static Tweener CreateTweener()
    {
        return new Tweener(static exception => throw new InvalidOperationException("Unexpected tween exception", exception));
    }
}
