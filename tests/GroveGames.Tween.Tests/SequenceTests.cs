using GroveGames.Tween.Core;
using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Tests;

public sealed class SequenceTests
{
    private class TestTween : ITween
    {
        public float Duration { get; }
        public bool IsPaused { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsRunning { get; private set; } = true;
        public int Id { get; private set; } = -1;

        public TestTween(float duration)
        {
            Duration = duration;
        }

        public void Pause()
        {
            IsPaused = true;
            IsPlaying = false;
        }

        public void Play()
        {
            IsPaused = false;
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            IsRunning = false;
        }

        public void Update(float deltaTime) { }

        public void Reset()
        {
            IsPlaying = false;
            IsRunning = true;
            IsPaused = false;
        }

        public ITween SetEase(EaseType easeType) => this;
        public ITween SetLoops(LoopType loopType, int loopCount) => this;
        public ITween SetOnComplete(Action onComplete) => this;
        public ITween SetOnStop(Action onStop) => this;
        public ITween SetId(int id)
        {
            Id = id;
            return this;
        }
    }

    private class CompletableTestTween : ITween
    {
        public float Duration { get; }
        public bool IsPaused { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsRunning { get; private set; } = true;
        public int Id { get; private set; } = -1;

        private Action? _onComplete;

        public CompletableTestTween(float duration)
        {
            Duration = duration;
        }

        public void Pause()
        {
            IsPaused = true;
            IsPlaying = false;
        }

        public void Play()
        {
            IsPaused = false;
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            IsRunning = false;
        }

        public void Update(float deltaTime) { }

        public void Reset()
        {
            IsPlaying = false;
            IsRunning = true;
            IsPaused = false;
        }

        public void CompleteNow()
        {
            IsRunning = false;
            IsPlaying = false;
            _onComplete?.Invoke();
        }

        public ITween SetEase(EaseType easeType) => this;
        public ITween SetLoops(LoopType loopType, int loopCount) => this;
        public ITween SetOnComplete(Action onComplete)
        {
            _onComplete += onComplete;
            return this;
        }
        public ITween SetOnStop(Action onStop) => this;
        public ITween SetId(int id)
        {
            Id = id;
            return this;
        }
    }

    [Fact]
    public void Constructor_DefaultState_InitializesCorrectly()
    {
        // Arrange & Act
        var sequence = new Sequence();

        // Assert
        Assert.True(sequence.IsRunning);
        Assert.True(sequence.IsPlaying);
        Assert.Equal(0f, sequence.Duration);
        Assert.Equal(-1, sequence.Id);
    }

    [Fact]
    public void Construct_AutoPlayTrue_SetsCorrectState()
    {
        // Arrange
        var sequence = new Sequence();

        // Act
        sequence.Construct(autoPlay: true);

        // Assert
        Assert.True(sequence.IsRunning);
        Assert.True(sequence.IsPlaying);
    }

    [Fact]
    public void Construct_AutoPlayFalse_SetsCorrectState()
    {
        // Arrange
        var sequence = new Sequence();

        // Act
        sequence.Construct(autoPlay: false);

        // Assert
        Assert.True(sequence.IsRunning);
        Assert.False(sequence.IsPlaying);
    }

    [Fact]
    public void Then_SingleTween_AddsTweenSequentially()
    {
        // Arrange
        var sequence = new Sequence();
        var testTween = new TestTween(2.0f);

        // Act
        var result = sequence.Then(testTween);

        // Assert
        Assert.Same(sequence, result);
        Assert.Equal(2.0f, sequence.Duration);
        Assert.True(testTween.IsPaused);
    }

    [Fact]
    public void Then_MultipleTweens_AddsTweensSequentially()
    {
        // Arrange
        var sequence = new Sequence();
        var tween1 = new TestTween(1.0f);
        var tween2 = new TestTween(2.0f);

        // Act
        sequence.Then(tween1).Then(tween2);

        // Assert
        Assert.Equal(3.0f, sequence.Duration);
        Assert.True(tween1.IsPaused);
        Assert.True(tween2.IsPaused);
    }

    [Fact]
    public void With_AfterThen_AddsTweenInParallel()
    {
        // Arrange
        var sequence = new Sequence();
        var tween1 = new TestTween(2.0f);
        var tween2 = new TestTween(1.0f);

        // Act
        sequence.Then(tween1).With(tween2);

        // Assert
        Assert.Equal(2.0f, sequence.Duration);
        Assert.True(tween1.IsPaused);
        Assert.True(tween2.IsPaused);
    }

    [Fact]
    public void Callback_AddsCallbackToSequence()
    {
        // Arrange
        var sequence = new Sequence();
        static void TestCallback() { }

        // Act
        var result = sequence.Callback(TestCallback);

        // Assert
        Assert.Same(sequence, result);
    }

    [Fact]
    public void Wait_AddsDurationToSequence()
    {
        // Arrange
        var sequence = new Sequence();
        var tween = new TestTween(1.0f);

        // Act
        sequence.Then(tween).Wait(2.0f);

        // Assert
        Assert.Equal(3.0f, sequence.Duration);
    }

    [Fact]
    public void Pause_SetsPauseState()
    {
        // Arrange
        var sequence = new Sequence();

        // Act
        sequence.Pause();

        // Assert
        Assert.False(sequence.IsPlaying);
        Assert.True(sequence.IsRunning);
    }

    [Fact]
    public void Play_SetsPlayState()
    {
        // Arrange
        var sequence = new Sequence();
        sequence.Pause();

        // Act
        sequence.Play();

        // Assert
        Assert.True(sequence.IsPlaying);
        Assert.True(sequence.IsRunning);
    }

    [Fact]
    public void SetEase_ThrowsArgumentException()
    {
        // Arrange
        var sequence = new Sequence();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => sequence.SetEase(EaseType.Linear));
    }

    [Fact]
    public void SetLoops_ThrowsArgumentException()
    {
        // Arrange
        var sequence = new Sequence();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => sequence.SetLoops(LoopType.Restart, 2));
    }

    [Fact]
    public void SetOnComplete_SetsCallback()
    {
        // Arrange
        var sequence = new Sequence();
        static void TestCallback() { }

        // Act
        var result = sequence.SetOnComplete(TestCallback);

        // Assert
        Assert.Same(sequence, result);
    }

    [Fact]
    public void SetOnStop_SetsCallback()
    {
        // Arrange
        var sequence = new Sequence();
        static void TestCallback() { }

        // Act
        var result = sequence.SetOnStop(TestCallback);

        // Assert
        Assert.Same(sequence, result);
    }

    [Fact]
    public void SetId_SetsId()
    {
        // Arrange
        var sequence = new Sequence();

        // Act
        var result = sequence.SetId(42);

        // Assert
        Assert.Same(sequence, result);
        Assert.Equal(42, sequence.Id);
    }

    [Fact]
    public void Stop_StopsRunningAndCallsOnStop()
    {
        // Arrange
        var sequence = new Sequence();
        var onStopCalled = false;
        sequence.SetOnStop(() => onStopCalled = true);

        // Act
        sequence.Stop();

        // Assert
        Assert.False(sequence.IsRunning);
        Assert.True(onStopCalled);
    }

    [Fact]
    public void Update_WhenPaused_DoesNotProgress()
    {
        // Arrange
        var sequence = new Sequence();
        var tween = new TestTween(1.0f);
        sequence.Then(tween);
        sequence.Pause();

        // Act
        sequence.Update(0.5f);

        // Assert
        Assert.False(tween.IsPlaying);
    }

    [Fact]
    public void Update_WhenStopped_DoesNotProgress()
    {
        // Arrange
        var sequence = new Sequence();
        var tween = new TestTween(1.0f);
        sequence.Then(tween);
        sequence.Stop();

        // Act
        sequence.Update(0.5f);

        // Assert
        Assert.False(tween.IsPlaying);
    }

    [Fact]
    public void Update_ProgressesTweensInOrder()
    {
        // Arrange
        var sequence = new Sequence();
        var tween1 = new CompletableTestTween(1.0f);
        var tween2 = new CompletableTestTween(1.0f);
        sequence.Then(tween1).Then(tween2);

        // Act
        sequence.Update(0.5f);

        // Assert
        Assert.Equal(2.0f, sequence.Duration);
        Assert.True(sequence.IsRunning);
    }

    [Fact]
    public void Update_CompletesSequenceAfterAllTweensComplete()
    {
        // Arrange
        var sequence = new Sequence();
        var tween1 = new CompletableTestTween(1.0f);
        var tween2 = new CompletableTestTween(1.0f);
        var onCompleteCalled = false;

        sequence.Then(tween1)
                .Then(tween2)
                .SetOnComplete(() => onCompleteCalled = true);

        // Act
        sequence.Update(1.0f);
        sequence.Update(0.1f);
        tween1.CompleteNow();

        Assert.False(onCompleteCalled);
        Assert.True(sequence.IsRunning);

        sequence.Update(1.0f);
        sequence.Update(0.1f);
        tween2.CompleteNow();

        sequence.Update(0.1f);

        // Assert
        Assert.True(onCompleteCalled);
        Assert.False(sequence.IsRunning);
    }

    [Fact]
    public void Update_ParallelTweens_AllExecuteAtSameTime()
    {
        // Arrange
        var sequence = new Sequence();
        var tween1 = new CompletableTestTween(2.0f);
        var tween2 = new CompletableTestTween(1.0f);
        sequence.Then(tween1).With(tween2);

        // Act
        sequence.Update(0.1f);

        // Assert
        Assert.Equal(2.0f, sequence.Duration);
        Assert.True(sequence.IsRunning);
    }

    [Fact]
    public void Update_CallbackExecutesAtCorrectTime()
    {
        // Arrange
        var sequence = new Sequence();
        var tween = new CompletableTestTween(1.0f);
        var callbackExecuted = false;

        sequence.Then(tween)
                .Callback(() => callbackExecuted = true);

        // Act
        sequence.Update(0.5f);
        Assert.False(callbackExecuted);

        sequence.Update(0.5f);
        tween.CompleteNow();
        sequence.Update(0.1f);

        // Assert
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void Update_WaitDelay_DelaysSubsequentTweens()
    {
        // Arrange
        var sequence = new Sequence();
        var tween1 = new CompletableTestTween(1.0f);
        var tween2 = new CompletableTestTween(1.0f);

        sequence.Then(tween1)
                .Wait(1.0f)
                .Then(tween2);

        // Act & Assert
        Assert.Equal(3.0f, sequence.Duration);

        sequence.Update(0.5f);
    }

    [Fact]
    public void Reset_ClearsAllState()
    {
        // Arrange
        var sequence = new Sequence();
        var tween = new TestTween(1.0f);
        sequence.Then(tween)
                .SetId(42)
                .SetOnComplete(() => { })
                .SetOnStop(() => { });

        // Act
        sequence.Reset();

        // Assert
        Assert.Equal(0f, sequence.Duration);
        Assert.Equal(-1, sequence.Id);
        Assert.False(sequence.IsPlaying);
        Assert.False(sequence.IsRunning);
    }

    [Fact]
    public void Update_MultipleOnCompleteCallbacks_AllExecuteAfterCompletion()
    {
        // Arrange
        var sequence = new Sequence();
        var tween = new CompletableTestTween(1.0f);
        var callback1Called = false;
        var callback2Called = false;

        sequence.Then(tween)
                .SetOnComplete(() => callback1Called = true)
                .SetOnComplete(() => callback2Called = true);

        // Act
        sequence.Update(1.0f);
        sequence.Update(0.1f);
        tween.CompleteNow();
        sequence.Update(0.1f);

        // Assert
        Assert.True(callback1Called);
        Assert.True(callback2Called);
        Assert.False(sequence.IsRunning);
    }

    [Fact]
    public void Update_EmptySequence_CompletesImmediately()
    {
        // Arrange
        var sequence = new Sequence();
        var onCompleteCalled = false;
        sequence.SetOnComplete(() => onCompleteCalled = true);

        // Act
        sequence.Update(0.1f);

        // Assert
        Assert.True(onCompleteCalled);
        Assert.False(sequence.IsRunning);
    }
}