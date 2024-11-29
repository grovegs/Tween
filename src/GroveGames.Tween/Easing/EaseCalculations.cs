using System;

namespace GroveGames.Tween.Easing;

public static class EaseCalculations
{
    public static float Evaluate(EaseType easeType, float t)
    {
        return easeType switch
        {
            EaseType.Linear => Linear(t),
            EaseType.InQuad => EaseInQuad(t),
            EaseType.OutQuad => EaseOutQuad(t),
            EaseType.InOutQuad => EaseInOutQuad(t),
            EaseType.InCubic => EaseInCubic(t),
            EaseType.OutCubic => EaseOutCubic(t),
            EaseType.InOutCubic => EaseInOutCubic(t),
            EaseType.InQuart => EaseInQuart(t),
            EaseType.OutQuart => EaseOutQuart(t),
            EaseType.InOutQuart => EaseInOutQuart(t),
            EaseType.InQuint => EaseInQuint(t),
            EaseType.OutQuint => EaseOutQuint(t),
            EaseType.InOutQuint => EaseInOutQuint(t),
            EaseType.InSine => EaseInSine(t),
            EaseType.OutSine => EaseOutSine(t),
            EaseType.InOutSine => EaseInOutSine(t),
            EaseType.InExpo => EaseInExpo(t),
            EaseType.OutExpo => EaseOutExpo(t),
            EaseType.InOutExpo => EaseInOutExpo(t),
            EaseType.InCirc => EaseInCirc(t),
            EaseType.OutCirc => EaseOutCirc(t),
            EaseType.InOutCirc => EaseInOutCirc(t),
            EaseType.InElastic => EaseInElastic(t),
            EaseType.OutElastic => EaseOutElastic(t),
            EaseType.InOutElastic => EaseInOutElastic(t),
            EaseType.InBack => EaseInBack(t),
            EaseType.OutBack => EaseOutBack(t),
            EaseType.InOutBack => EaseInOutBack(t),
            EaseType.InBounce => EaseInBounce(t),
            EaseType.OutBounce => EaseOutBounce(t),
            EaseType.InOutBounce => EaseInOutBounce(t),
            _ => Linear(t)
        };
    }

    // Linear
    private static float Linear(float t) => t;

    // Quad
    private static float EaseInQuad(float t) => t * t;
    private static float EaseOutQuad(float t) => t * (2 - t);
    private static float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

    // Cubic
    private static float EaseInCubic(float t) => t * t * t;
    private static float EaseOutCubic(float t) => (--t) * t * t + 1;
    private static float EaseInOutCubic(float t) => t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;

    // Quart
    private static float EaseInQuart(float t) => t * t * t * t;
    private static float EaseOutQuart(float t) => 1 - (--t) * t * t * t;
    private static float EaseInOutQuart(float t) => t < 0.5f ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;

    // Quint
    private static float EaseInQuint(float t) => t * t * t * t * t;
    private static float EaseOutQuint(float t) => 1 + (--t) * t * t * t * t;
    private static float EaseInOutQuint(float t) => t < 0.5f ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;

    // Sine
    private static float EaseInSine(float t) => 1 - (float)Math.Cos(t * Math.PI / 2);
    private static float EaseOutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
    private static float EaseInOutSine(float t) => 0.5f * (1 - (float)Math.Cos(Math.PI * t));

    // Expo
    private static float EaseInExpo(float t) => t == 0 ? 0 : (float)Math.Pow(2, 10 * (t - 1));
    private static float EaseOutExpo(float t) => t == 1 ? 1 : 1 - (float)Math.Pow(2, -10 * t);
    private static float EaseInOutExpo(float t) =>
        t == 0 ? 0 : t == 1 ? 1 : t < 0.5f ? (float)Math.Pow(2, 20 * t - 10) / 2 : (2 - (float)Math.Pow(2, -20 * t + 10)) / 2;

    // Circ
    private static float EaseInCirc(float t) => 1 - (float)Math.Sqrt(1 - t * t);
    private static float EaseOutCirc(float t) => (float)Math.Sqrt(1 - (--t) * t);
    private static float EaseInOutCirc(float t) =>
        t < 0.5f ? (1 - (float)Math.Sqrt(1 - 4 * t * t)) / 2 : ((float)Math.Sqrt(1 - (--t) * (2 * t - 2)) + 1) / 2;

    // Elastic
    private static float EaseInElastic(float t) =>
        t == 0 || t == 1 ? t : -(float)Math.Pow(2, 10 * (t - 1)) * (float)Math.Sin((t - 1.1f) * 5 * Math.PI);
    private static float EaseOutElastic(float t) =>
        t == 0 || t == 1 ? t : (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - 0.1f) * 5 * Math.PI) + 1;
    private static float EaseInOutElastic(float t) =>
        t == 0 || t == 1
            ? t
            : t < 0.5f
                ? -(float)Math.Pow(2, 20 * t - 10) * (float)Math.Sin((20 * t - 11.125f) * Math.PI / 4.5f) / 2
                : (float)Math.Pow(2, -20 * t + 10) * (float)Math.Sin((20 * t - 11.125f) * Math.PI / 4.5f) / 2 + 1;

    // Back
    private static float EaseInBack(float t, float s = 1.70158f) => t * t * ((s + 1) * t - s);
    private static float EaseOutBack(float t, float s = 1.70158f) => (--t) * t * ((s + 1) * t + s) + 1;
    private static float EaseInOutBack(float t, float s = 1.70158f) =>
        t < 0.5f ? t * t * ((s + 1) * 2 * t - s) / 2 : ((--t) * t * ((s + 1) * 2 * t + s) + 2) / 2;

    // Bounce
    private static float EaseInBounce(float t) => 1 - EaseOutBounce(1 - t);
    private static float EaseOutBounce(float t)
    {
        if (t < 1 / 2.75f) return 7.5625f * t * t;
        if (t < 2 / 2.75f) return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
        if (t < 2.5f / 2.75f) return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
        return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
    }
    private static float EaseInOutBounce(float t) =>
        t < 0.5f ? EaseInBounce(t * 2) * 0.5f : EaseOutBounce(t * 2 - 1) * 0.5f + 0.5f;
}