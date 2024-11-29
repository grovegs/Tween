using System;

namespace GroveGames.Tween.Easing;

public static class EaseCalculations
{
    private const float PI = MathF.PI;
    private const float C1 = 1.70158f;
    private const float C2 = C1 * 1.525f;
    private const float C3 = C1 + 1;
    private const float C4 = 2 * PI / 3;
    private const float C5 = 2 * PI / 4.5f;

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

    public static float Linear(float x) => x;

    public static float EaseInQuad(float x) => x * x;

    public static float EaseOutQuad(float x) => 1 - (1 - x) * (1 - x);

    public static float EaseInOutQuad(float x) =>
        x < 0.5f ? 2 * x * x : 1 - MathF.Pow(-2 * x + 2, 2) / 2;

    public static float EaseInCubic(float x) => x * x * x;

    public static float EaseOutCubic(float x) => 1 - MathF.Pow(1 - x, 3);

    public static float EaseInOutCubic(float x) =>
        x < 0.5f ? 4 * x * x * x : 1 - MathF.Pow(-2 * x + 2, 3) / 2;

    public static float EaseInQuart(float x) => x * x * x * x;

    public static float EaseOutQuart(float x) => 1 - MathF.Pow(1 - x, 4);

    public static float EaseInOutQuart(float x) =>
        x < 0.5f ? 8 * x * x * x * x : 1 - MathF.Pow(-2 * x + 2, 4) / 2;

    public static float EaseInQuint(float x) => x * x * x * x * x;

    public static float EaseOutQuint(float x) => 1 - MathF.Pow(1 - x, 5);

    public static float EaseInOutQuint(float x) =>
        x < 0.5f ? 16 * x * x * x * x * x : 1 - MathF.Pow(-2 * x + 2, 5) / 2;

    public static float EaseInSine(float x) => 1 - MathF.Cos(x * PI / 2);

    public static float EaseOutSine(float x) => MathF.Sin(x * PI / 2);

    public static float EaseInOutSine(float x) => -(MathF.Cos(PI * x) - 1) / 2;

    public static float EaseInExpo(float x) => x == 0 ? 0 : MathF.Pow(2, 10 * x - 10);

    public static float EaseOutExpo(float x) => x == 1 ? 1 : 1 - MathF.Pow(2, -10 * x);

    public static float EaseInOutExpo(float x) =>
        x == 0 ? 0 :
        x == 1 ? 1 :
        x < 0.5f ? MathF.Pow(2, 20 * x - 10) / 2 : (2 - MathF.Pow(2, -20 * x + 10)) / 2;

    public static float EaseInCirc(float x) => 1 - MathF.Sqrt(1 - x * x);

    public static float EaseOutCirc(float x) => MathF.Sqrt(1 - MathF.Pow(x - 1, 2));

    public static float EaseInOutCirc(float x) =>
        x < 0.5f ? (1 - MathF.Sqrt(1 - MathF.Pow(2 * x, 2))) / 2 :
                  (MathF.Sqrt(1 - MathF.Pow(-2 * x + 2, 2)) + 1) / 2;

    public static float EaseInBack(float x) => C3 * x * x * x - C1 * x * x;

    public static float EaseOutBack(float x) => 1 + C3 * MathF.Pow(x - 1, 3) + C1 * MathF.Pow(x - 1, 2);

    public static float EaseInOutBack(float x) =>
        x < 0.5f ? MathF.Pow(2 * x, 2) * ((C2 + 1) * 2 * x - C2) / 2 :
                  (MathF.Pow(2 * x - 2, 2) * ((C2 + 1) * (2 * x - 2) + C2) + 2) / 2;

    public static float EaseInElastic(float x) =>
        x == 0 ? 0 :
        x == 1 ? 1 :
        -MathF.Pow(2, 10 * x - 10) * MathF.Sin((x * 10 - 10.75f) * C4);

    public static float EaseOutElastic(float x) =>
        x == 0 ? 0 :
        x == 1 ? 1 :
        MathF.Pow(2, -10 * x) * MathF.Sin((x * 10 - 0.75f) * C4) + 1;

    public static float EaseInOutElastic(float x) =>
        x == 0 ? 0 :
        x == 1 ? 1 :
        x < 0.5f ? -(MathF.Pow(2, 20 * x - 10) * MathF.Sin((20 * x - 11.125f) * C5)) / 2 :
                  MathF.Pow(2, -20 * x + 10) * MathF.Sin((20 * x - 11.125f) * C5) / 2 + 1;

    public static float EaseInBounce(float x) => 1 - EaseOutBounce(1 - x);

    public static float EaseOutBounce(float x)
    {
        const float N1 = 7.5625f;
        const float D1 = 2.75f;

        if (x < 1 / D1) return N1 * x * x;
        if (x < 2 / D1) return N1 * (x -= 1.5f / D1) * x + 0.75f;
        if (x < 2.5f / D1) return N1 * (x -= 2.25f / D1) * x + 0.9375f;
        return N1 * (x -= 2.625f / D1) * x + 0.984375f;
    }

    public static float EaseInOutBounce(float x) =>
        x < 0.5f ? (1 - EaseOutBounce(1 - 2 * x)) / 2 :
                  (1 + EaseOutBounce(2 * x - 1)) / 2;
}