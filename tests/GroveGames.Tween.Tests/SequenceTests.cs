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
        sequence.Append(mockTween.Object);

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
        sequence.Append(mockTween1.Object).Join(mockTween2.Object);

        // Assert
        Assert.Equal(2f, sequence.Duration);
    }

    [Fact]
    public void Sequence_Update_Invokes_Tweens_And_Callbacks()
    {
        // Arrange
        var mockTween = new Mock<ITween>();
        mockTween.Setup(t => t.Duration).Returns(1f);
        mockTween.Setup(t => t.IsRunning).Returns(true);
        mockTween.Setup(t => t.IsPlaying).Returns(false);

        var callbackMock = new Mock<Action>();

        var sequence = new Sequence();
        sequence.Append(mockTween.Object).AppendCallback(callbackMock.Object);

        // Act
        sequence.Update(1f);

        // Assert
        mockTween.Verify(t => t.Play(), Times.Once);
        callbackMock.Verify(cb => cb.Invoke(), Times.Once);
    }
}