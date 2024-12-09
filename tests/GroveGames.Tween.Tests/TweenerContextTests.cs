using GroveGames.Tween.Context;
using GroveGames.Tween.Core;

namespace GroveGames.Tween.Tests;

public class TweenerContextTests
{
    [Fact]
    public void TweenerContext_CreateTween_Adds_Tween()
    {
        // Arrange
        var context = new TweenerContext();

        // Act
        var tween = context.CreateTween(
            () => 0f,
            () => 10f,
            1f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );

        // Assert
        Assert.NotNull(tween);
    }

    [Fact]
    public void TweenerContext_Stop_By_Id_Stops_Correct_Tween()
    {
        // Arrange
        var context = new TweenerContext();
        var tween1 = context.CreateTween(
            () => 0f,
            () => 10f,
            1f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );
        tween1.SetId(1);

        var tween2 = context.CreateTween(
            () => 10f,
            () => 20f,
            1f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );
        tween2.SetId(2);

        // Act
        context.Stop(1);

        context.Update(0f);

        // Assert
        Assert.False(tween1.IsRunning);
        Assert.True(tween2.IsRunning);
    }

    [Fact]
    public void TweenerContext_Update_Processes_Tweens()
    {
        // Arrange
        var context = new TweenerContext();
        var mockTween = new Mock<ITween>();
        mockTween.Setup(t => t.IsRunning).Returns(true);

        context.Stop(mockTween.Object);

        // Act
        context.Update(0.1f);

        // Assert
        mockTween.Verify(t => t.Update(It.IsAny<float>()), Times.Never);
    }
}