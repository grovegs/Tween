using GroveGames.Tween.Core;

namespace GroveGames.Tween.Tests;

public class TweenTests
{
    [Fact]
    public void Tween_Update_Invokes_OnUpdate_With_Correct_Value()
    {
        // Arrange
        var startValue = 0f;
        var endValue = 10f;
        var duration = 2f;

        var onUpdateMock = new Mock<Action<float>>();
        var tween = new Tween<float>(
            () => startValue,
            () => endValue,
            duration,
            (start, end, t) => start + (end - start) * t,
            autoStart: true
        );
        tween.SetOnUpdate(onUpdateMock.Object);

        // Act
        tween.Update(1f);

        // Assert
        onUpdateMock.Verify(update => update.Invoke(It.Is<float>(v => Math.Abs(v - 5f) < 0.01f)), Times.Once);
    }

    [Fact]
    public void Tween_Complete_Invokes_OnComplete()
    {
        // Arrange
        var startValue = 0f;
        var endValue = 10f;
        var duration = 1f;

        var onCompleteMock = new Mock<Action>();
        var tween = new Tween<float>(
            () => startValue,
            () => endValue,
            duration,
            (start, end, t) => start + (end - start) * t,
            autoStart: true
        );
        tween.SetOnComplete(onCompleteMock.Object);

        // Act
        tween.Update(1f);

        // Assert
        onCompleteMock.Verify(complete => complete.Invoke(), Times.Once);
    }

    [Fact]
    public void Tween_Stop_Stops_Tween()
    {
        // Arrange
        var startValue = 0f;
        var endValue = 10f;
        var duration = 2f;

        var onUpdateMock = new Mock<Action<float>>();
        var tween = new Tween<float>(
            () => startValue,
            () => endValue,
            duration,
            (start, end, t) => start + (end - start) * t,
            autoStart: true
        );
        tween.SetOnUpdate(onUpdateMock.Object);

        // Act
        tween.Stop(false);
        tween.Update(1f);

        // Assert
        onUpdateMock.Verify(update => update.Invoke(It.IsAny<float>()), Times.Never);
    }
}