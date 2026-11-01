using System;

namespace GroveGames.Tween.Tests;

public sealed class EasingTests
{
    private const float Tolerance = 0.0001f;

    [Fact]
    public void Evaluate_AllEases_StartAtZeroAndEndAtOne()
    {
        foreach (Ease ease in Enum.GetValues(typeof(Ease)))
        {
            var start = Easing.Evaluate(ease, 0f);
            var end = Easing.Evaluate(ease, 1f);

            Assert.Equal(0f, start, Tolerance);
            Assert.Equal(1f, end, Tolerance);
        }
    }

    [Theory]
    [InlineData(Ease.Linear, 0.5f)]
    [InlineData(Ease.InQuad, 0.25f)]
    [InlineData(Ease.OutQuad, 0.75f)]
    [InlineData(Ease.InOutQuad, 0.5f)]
    [InlineData(Ease.InCubic, 0.125f)]
    [InlineData(Ease.OutCubic, 0.875f)]
    [InlineData(Ease.InOutSine, 0.5f)]
    [InlineData(Ease.InOutBounce, 0.5f)]
    public void Evaluate_Midpoint_ReturnsKnownValue(Ease ease, float expected)
    {
        var value = Easing.Evaluate(ease, 0.5f);

        Assert.Equal(expected, value, Tolerance);
    }
}
