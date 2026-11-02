using System;
using System.Numerics;
using GroveGames.Scheduling;

namespace GroveGames.Tween.Tests;

public sealed class TweenerTests
{
    private readonly struct TestVector2Adapter : ITweenAdapter<Vector2>
    {
        public Vector2 Evaluate(in Vector2 from, in Vector2 to, float progress)
        {
            return Vector2.Lerp(from, to, progress);
        }
    }

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
    private static readonly Action<float, TestTarget> s_ignore = static (_, _) => { };

    [Fact]
    public void Constructor_NullExceptionHandler_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Tweener(null!));
    }

    [Fact]
    public void Create_GenericAdapter_InterpolatesValue()
    {
        var tweener = CreateTweener();
        var value = Vector2.Zero;
        tweener.Create<Vector2, TestVector2Adapter>(Vector2.Zero, new Vector2(2f, 4f), 1f).Bind(new TestTarget(), (current, _) => value = current);

        Step(tweener, 0.5f);

        Assert.True(Vector2.Distance(value, new Vector2(1f, 2f)) < Tolerance);
    }

    [Fact]
    public void Create_GenericAdapterWithSettings_UsesSettings()
    {
        var tweener = CreateTweener();
        var value = Vector2.Zero;
        var settings = new TweenSettings(1f, loops: 2, loopType: LoopType.Yoyo);
        tweener.Create<Vector2, TestVector2Adapter>(Vector2.Zero, Vector2.One, in settings).Bind(new TestTarget(), (current, _) => value = current);

        Step(tweener, 1.5f);

        Assert.True(Vector2.Distance(value, new Vector2(0.5f, 0.5f)) < Tolerance);
    }

    [Fact]
    public void Create_TwoAdapterTypes_UpdatesBoth()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var vector = Vector2.Zero;
        tweener.Create<Vector2, TestVector2Adapter>(Vector2.Zero, Vector2.One, 1f).Bind(target, (current, _) => vector = current);
        tweener.Create(0f, 1f, 1f).Bind(target, s_setValue);

        Step(tweener, 0.25f);

        Assert.Equal(0.25f, target.Value, Tolerance);
        Assert.True(Vector2.Distance(vector, new Vector2(0.25f, 0.25f)) < Tolerance);
    }

    [Fact]
    public void Cancel_GenericAdapterTween_StopsTween()
    {
        var tweener = CreateTweener();
        var value = Vector2.Zero;
        var handle = tweener.Create<Vector2, TestVector2Adapter>(Vector2.Zero, Vector2.One, 1f).Bind(new TestTarget(), (current, _) => value = current);

        var isCanceled = handle.Cancel();
        Step(tweener, 0.5f);

        Assert.True(isCanceled);
        Assert.False(handle.IsActive);
        Assert.Equal(Vector2.Zero, value);
    }

    [Fact]
    public void Update_Punch_ReturnsToStartAtEnd()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.CreatePunch<float, FloatTweenAdapter>(1f, 2f, 1f).Bind(target, s_setValue);

        Step(tweener, 0.05f);
        var moved = target.Value;
        Step(tweener, 1f);

        Assert.NotEqual(1f, moved);
        Assert.Equal(1f, target.Value);
    }

    [Fact]
    public void Bind_WithoutDelay_AppliesStartValue()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();

        tweener.Create(2f, 4f, 1f).Bind(target, s_setValue);

        Assert.Equal(2f, target.Value);
    }

    [Fact]
    public void Bind_WithDelay_DoesNotApplyStartValue()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();

        tweener.Create(2f, 4f, 1f).WithDelay(0.5f).Bind(target, s_setValue);

        Assert.Equal(0, target.SetCount);
    }

    [Fact]
    public void Update_HalfDuration_AppliesEasedValue()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 1f).WithEase(Ease.OutQuad).Bind(target, s_setValue);

        Step(tweener, 0.5f);

        Assert.Equal(0.75f, target.Value, Tolerance);
    }

    [Fact]
    public void Update_PastDuration_AppliesEndValueAndCompletesOnce()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var handle = tweener.Create(0f, 1f, 1f).WithCompletionCallback(target.Complete).Bind(target, s_setValue);

        Step(tweener, 1.5f);
        Step(tweener, 1f);

        Assert.Equal(1f, target.Value);
        Assert.Equal(1, target.CompletedCount);
        Assert.False(handle.IsActive);
    }

    [Fact]
    public void Update_ScaledTimeMode_IgnoresUnscaledDeltaTime()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 1f).Bind(target, s_setValue);

        tweener.Update(0f, 0.5f, 0.5f);

        Assert.Equal(0f, target.Value);
    }

    [Fact]
    public void Update_UnscaledTimeMode_UsesUnscaledDeltaTime()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 1f).WithTimeMode(TimeMode.Unscaled).Bind(target, s_setValue);

        tweener.Update(0f, 0.5f, 0f);

        Assert.Equal(0.5f, target.Value, Tolerance);
    }

    [Fact]
    public void Update_RealtimeTimeMode_UsesRealtimeDeltaTime()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 1f).WithTimeMode(TimeMode.Realtime).Bind(target, s_setValue);

        tweener.Update(0f, 0f, 0.25f);

        Assert.Equal(0.25f, target.Value, Tolerance);
    }

    [Fact]
    public void Update_SettingsTimeMode_UsesSettingsTimeMode()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var settings = new TweenSettings(1f, timeMode: TimeMode.Unscaled);
        tweener.Create(0f, 1f, in settings).Bind(target, s_setValue);

        tweener.Update(0f, 0.5f, 0f);

        Assert.Equal(0.5f, target.Value, Tolerance);
    }

    [Fact]
    public void Update_DuringDelay_DoesNotApplyValue()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(2f, 4f, 1f).WithDelay(1f).Bind(target, s_setValue);

        Step(tweener, 0.5f);

        Assert.Equal(0, target.SetCount);
    }

    [Fact]
    public void Update_AfterDelay_CarriesOverTime()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 1f).WithDelay(0.5f).Bind(target, s_setValue);

        Step(tweener, 0.75f);

        Assert.Equal(0.25f, target.Value, Tolerance);
    }

    [Fact]
    public void Update_ZeroDuration_CompletesOnNextUpdate()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 0f).WithCompletionCallback(target.Complete).Bind(target, s_setValue);

        var completedBeforeUpdate = target.CompletedCount;
        Step(tweener, 0f);

        Assert.Equal(0, completedBeforeUpdate);
        Assert.Equal(1f, target.Value);
        Assert.Equal(1, target.CompletedCount);
    }

    [Fact]
    public void Update_RestartLoops_RestartsFromStart()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 1f).WithLoops(2).Bind(target, s_setValue);

        Step(tweener, 1.25f);

        Assert.Equal(0.25f, target.Value, Tolerance);
    }

    [Fact]
    public void Update_YoyoLoops_PlaysBackwardAndEndsAtStart()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 1f).WithLoops(2, LoopType.Yoyo).Bind(target, s_setValue);

        Step(tweener, 1.25f);
        var backward = target.Value;
        Step(tweener, 1f);

        Assert.Equal(0.75f, backward, Tolerance);
        Assert.Equal(0f, target.Value);
    }

    [Fact]
    public void Update_InfiniteLoops_NeverCompletes()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var handle = tweener.Create(0f, 360f, 1f).WithLoops(-1).WithCompletionCallback(target.Complete).Bind(target, s_setValue);

        for (var i = 0; i < 100; i++)
        {
            Step(tweener, 0.37f);
        }

        Assert.True(handle.IsActive);
        Assert.Equal(0, target.CompletedCount);
        Assert.Equal(0f, target.Value, 0.01f);
    }

    [Fact]
    public void Update_LoopInterval_HoldsEndValueThenRestarts()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        tweener.Create(0f, 1f, 1f).WithLoops(-1, LoopType.Restart, 2f).Bind(target, s_setValue);

        Step(tweener, 2f);
        var during = target.Value;
        Step(tweener, 1.5f);

        Assert.Equal(1f, during);
        Assert.Equal(0.5f, target.Value, Tolerance);
    }

    [Fact]
    public void Update_FiniteLoopsWithInterval_CompletesWithoutTrailingInterval()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var handle = tweener.Create(0f, 1f, 1f).WithLoops(2, LoopType.Restart, 2f).WithCompletionCallback(target.Complete).Bind(target, s_setValue);

        Step(tweener, 3.9f);
        var activeBeforeEnd = handle.IsActive;
        Step(tweener, 0.1f);

        Assert.True(activeBeforeEnd);
        Assert.Equal(1, target.CompletedCount);
        Assert.Equal(1f, target.Value);
    }

    [Fact]
    public void Update_SettingsLoops_UsesSettingsLoops()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var settings = new TweenSettings(1f, loops: 2, loopType: LoopType.Yoyo);
        tweener.Create(0f, 1f, in settings).Bind(target, s_setValue);

        Step(tweener, 1.5f);

        Assert.Equal(0.5f, target.Value, Tolerance);
    }




    [Fact]
    public void Cancel_ActiveTween_StopsWithoutCompleting()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var handle = tweener.Create(0f, 1f, 1f).WithCompletionCallback(target.Complete).Bind(target, s_setValue);

        var isCanceled = handle.Cancel();
        Step(tweener, 2f);

        Assert.True(isCanceled);
        Assert.Equal(0f, target.Value);
        Assert.Equal(0, target.CompletedCount);
        Assert.False(handle.IsActive);
    }

    [Fact]
    public void Cancel_CanceledTween_ReturnsFalse()
    {
        var tweener = CreateTweener();
        var handle = tweener.Create(0f, 1f, 1f).Bind(new TestTarget(), s_setValue);
        handle.Cancel();

        var isCanceled = handle.Cancel();

        Assert.False(isCanceled);
    }

    [Fact]
    public void Cancel_DefaultHandle_ReturnsFalse()
    {
        var handle = default(TweenHandle);

        var isCanceled = handle.Cancel();

        Assert.False(isCanceled);
        Assert.False(handle.IsActive);
    }

    [Fact]
    public void Cancel_StaleHandleAfterSlotReuse_DoesNotCancelNewTween()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var stale = tweener.Create(0f, 1f, 1f).Bind(target, s_ignore);
        stale.Cancel();
        var current = tweener.Create(0f, 1f, 1f).Bind(target, s_setValue);

        var isCanceled = stale.Cancel();
        Step(tweener, 0.5f);

        Assert.False(isCanceled);
        Assert.True(current.IsActive);
        Assert.Equal(0.5f, target.Value, Tolerance);
    }

    [Fact]
    public void WithToken_CanceledAfterBind_StopsTween()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var token = new ScheduleToken();
        var handle = tweener.Create(0f, 1f, 1f).WithToken(token).WithCompletionCallback(target.Complete).Bind(target, s_setValue);

        token.Cancel();
        Step(tweener, 2f);

        Assert.False(handle.IsActive);
        Assert.Equal(0f, target.Value);
        Assert.Equal(0, target.CompletedCount);
    }

    [Fact]
    public void WithToken_CanceledBeforeBind_ReturnsInactiveHandle()
    {
        var tweener = CreateTweener();
        var target = new TestTarget();
        var token = new ScheduleToken();
        token.Cancel();

        var handle = tweener.Create(0f, 1f, 1f).WithToken(token).Bind(target, s_setValue);

        Assert.False(handle.IsActive);
        Assert.Equal(0, target.SetCount);
    }

    [Fact]
    public void WithToken_ResetAfterCancel_RunsNewTweensOnly()
    {
        var tweener = CreateTweener();
        var token = new ScheduleToken();
        var old = tweener.Create(0f, 1f, 1f).WithToken(token).Bind(new TestTarget(), s_ignore);
        token.Cancel();
        token.Reset();

        var current = tweener.Create(0f, 1f, 1f).WithToken(token).Bind(new TestTarget(), s_ignore);

        Assert.False(old.IsActive);
        Assert.True(current.IsActive);
    }

    [Fact]
    public void WithCompletionCallback_CreatesTween_KeepsOtherTweensUpdating()
    {
        var tweener = CreateTweener();
        var first = new TestTarget();
        var second = new TestTarget();
        var chained = new TestTarget();
        tweener.Create(0f, 1f, 0.5f).WithCompletionCallback(() => tweener.Create(0f, 1f, 1f).Bind(chained, s_setValue)).Bind(first, s_setValue);
        tweener.Create(0f, 1f, 1f).Bind(second, s_setValue);

        Step(tweener, 0.5f);
        Step(tweener, 0.25f);

        Assert.Equal(1f, first.Value);
        Assert.Equal(0.75f, second.Value, Tolerance);
        Assert.Equal(0.25f, chained.Value, Tolerance);
    }

    [Fact]
    public void Setter_CancelsEarlierTween_KeepsOtherTweensUpdating()
    {
        var tweener = CreateTweener();
        var first = new TestTarget();
        var third = new TestTarget();
        var firstHandle = tweener.Create(0f, 1f, 1f).Bind(first, s_setValue);
        tweener.Create(0f, 1f, 1f).Bind(new TestTarget(), (value, _) =>
        {
            if (value > 0f)
            {
                firstHandle.Cancel();
            }
        });
        tweener.Create(0f, 1f, 1f).Bind(third, s_setValue);

        Step(tweener, 0.25f);
        Step(tweener, 0.25f);

        Assert.False(firstHandle.IsActive);
        Assert.Equal(0.25f, first.Value, Tolerance);
        Assert.Equal(0.5f, third.Value, Tolerance);
    }

    [Fact]
    public void Update_ManyTweens_RemovesCompletedAndKeepsOrder()
    {
        var tweener = CreateTweener();
        var targets = new TestTarget[100];

        for (var i = 0; i < targets.Length; i++)
        {
            targets[i] = new TestTarget();
            tweener.Create(0f, 1f, i % 2 == 0 ? 0.5f : 1f).Bind(targets[i], s_setValue);
        }

        Step(tweener, 0.75f);

        for (var i = 0; i < targets.Length; i++)
        {
            Assert.Equal(i % 2 == 0 ? 1f : 0.75f, targets[i].Value, Tolerance);
        }
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
