using GroveGames.Tween.Context;
using GroveGames.Tween.Core;

using Xunit;

namespace GroveGames.Tween.Tests;

public class TweenerContextTests
{
    [Fact]
    public void TweenerContext_CreateTween_Reuses_Pooled_Tween()
    {
        // Arrange
        var context = new TweenerContext();

        // Act
        var tween1 = context.CreateTween(
            () => 0f,
            () => 10f,
            1f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );

        context.Stop(tween1);
        context.Update(0f);

        var tween2 = context.CreateTween(
            () => 5f,
            () => 15f,
            2f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );

        // Assert
        Assert.Same(tween1, tween2);
    }

    [Fact]
    public void TweenerContext_CreateTween_Creates_New_Tween_If_Pool_Empty()
    {
        // Arrange
        var context = new TweenerContext();

        // Act
        var tween1 = context.CreateTween(
            () => 0f,
            () => 10f,
            1f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );

        var tween2 = context.CreateTween(
            () => 5f,
            () => 15f,
            2f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );

        // Assert
        Assert.NotSame(tween1, tween2);
    }

    [Fact]
    public void TweenerContext_Stop_Marks_Tween_As_Stopped()
    {
        // Arrange
        var context = new TweenerContext();

        var tween = context.CreateTween(
            () => 0f,
            () => 10f,
            1f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );

        // Act
        context.Stop(tween);
        context.Update(0f);

        // Assert
        Assert.False(tween.IsRunning);
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
            () => 5f,
            () => 15f,
            2f,
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
    public void TweenerContext_Update_Processes_Running_Tweens()
    {
        // Arrange
        var context = new TweenerContext();

        var tween = context.CreateTween(
            () => 0f,
            () => 10f,
            1f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );

        // Act
        context.Update(0.5f);

        // Assert
        Assert.True(tween.IsRunning);
    }

    [Fact]
    public void TweenerContext_Update_Removes_Stopped_Tweens()
    {
        // Arrange
        var context = new TweenerContext();

        var tween = context.CreateTween(
            () => 0f,
            () => 10f,
            1f,
            (start, end, t) => start + (end - start) * t,
            autoPlay: true
        );

        context.Stop(tween);

        // Act
        context.Update(0.1f);

        // Assert
        Assert.False(tween.IsRunning);
    }
}
