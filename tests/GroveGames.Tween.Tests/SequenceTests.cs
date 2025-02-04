using GroveGames.Tween.Core;

namespace GroveGames.Tween.Tests;

public class SequenceTests
{
    [Fact]
    public void Sequence_Append_Adds_Tweens()
    {
        // Arrange
        var mockTween = new Mock<ITween>();
        mockTween.Setup(t => t.Duration).Returns(1f);

        var sequence = new Sequence();

        // Act
        sequence.Then(mockTween.Object);

        // Assert
        Assert.Equal(1f, sequence.Duration);
    }

    [Fact]
    public void Sequence_Join_Adds_Parallel_Tweens()
    {
        // Arrange
        var mockTween1 = new Mock<ITween>();
        mockTween1.Setup(t => t.Duration).Returns(2f);

        var mockTween2 = new Mock<ITween>();
        mockTween2.Setup(t => t.Duration).Returns(1f);

        var sequence = new Sequence();

        // Act
        sequence.Then(mockTween1.Object).With(mockTween2.Object);

        // Assert
        Assert.Equal(2f, sequence.Duration);
    }

    [Fact]
    public void Sequence_Update_Invokes_Tweens_And_Callbacks()
    {
        // Arrange
        var mockTween1 = new Mock<ITween>();
        mockTween1.Setup(t => t.Duration).Returns(1f);
        mockTween1.Setup(t => t.IsRunning).Returns(true);
        mockTween1.Setup(t => t.IsPlaying).Returns(false);

        var mockTween2 = new Mock<ITween>();
        mockTween2.Setup(t => t.Duration).Returns(1f);
        mockTween2.Setup(t => t.IsRunning).Returns(true);
        mockTween2.Setup(t => t.IsPlaying).Returns(false);

        var mockTween3 = new Mock<ITween>();
        mockTween3.Setup(t => t.Duration).Returns(1f);
        mockTween3.Setup(t => t.IsRunning).Returns(true);
        mockTween3.Setup(t => t.IsPlaying).Returns(false);

        var mockTween4 = new Mock<ITween>();
        mockTween4.Setup(t => t.Duration).Returns(1f);
        mockTween4.Setup(t => t.IsRunning).Returns(true);
        mockTween4.Setup(t => t.IsPlaying).Returns(false);

        var callbackMock = new Mock<Action>();

        var sequence = new Sequence();
        sequence
        .Then(mockTween1.Object)
        .Then(mockTween2.Object)
        .With(mockTween3.Object)
        .Callback(callbackMock.Object)
        .Wait(1f)
        .Then(mockTween4.Object);

        // Act
        sequence.Update(0.1f);
        mockTween1.Verify(t => t.Play(), Times.Once);

        sequence.Update(1f);
        mockTween2.Verify(t => t.Play(), Times.Once);
        mockTween3.Verify(t => t.Play(), Times.Once);

        sequence.Update(0.9f);
        callbackMock.Verify(cb => cb.Invoke(), Times.Once);

        mockTween4.Verify(t => t.Play(), Times.Never);
    }

    [Fact]
    public void Sequence_Stop_Stops_Tweens()
    {
        // Arrange
        var onStopMock = new Mock<Action>();
        var onStopMock2 = new Mock<Action>();
        var onStopMock3 = new Mock<Action>();

        var startValue = 0f;
        var endValue = 10f;
        var duration = 1f;

        var tween1 = new Tween<float>();
        tween1.Construct(
            () => startValue,
            () => endValue,
            duration,
            (start, end, t) => start + (end - start) * t,
            autoStart: false
        );
        tween1.SetOnStop(onStopMock.Object);

        var tween2 = new Tween<float>();
        tween2.Construct(
            () => startValue,
            () => endValue,
            duration,
            (start, end, t) => start + (end - start) * t,
            autoStart: false
        );
        tween2.SetOnStop(onStopMock2.Object);

        var tween3 = new Tween<float>();
        tween3.Construct(
            () => startValue,
            () => endValue,
            duration,
            (start, end, t) => start + (end - start) * t,
            autoStart: false
        );
        tween3.SetOnStop(onStopMock3.Object);

        var sequence = new Sequence();
        sequence
        .Then(tween1)
        .Then(tween2)
        .With(tween3);

        // Act
        sequence.Update(1.1f);
        tween1.Update(1.1f);
        sequence.Stop();

        onStopMock.Verify(stop => stop.Invoke(), Times.Never);
        onStopMock2.Verify(stop => stop.Invoke(), Times.Once);
        onStopMock3.Verify(stop => stop.Invoke(), Times.Once);
    }
}