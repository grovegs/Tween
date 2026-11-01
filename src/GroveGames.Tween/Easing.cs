namespace GroveGames.Tween;

public static class Easing
{
    private const float BackOvershoot = 1.70158f;
    private const float BackInOutOvershoot = BackOvershoot * 1.525f;
    private const float ElasticPeriod = 2f * MathF.PI / 3f;
    private const float ElasticInOutPeriod = 2f * MathF.PI / 4.5f;
    private const float BounceScale = 7.5625f;
    private const float BounceDivisor = 2.75f;

    public static float Evaluate(Ease ease, float t)
    {
        return ease switch
        {
            Ease.InSine => 1f - MathF.Cos(t * MathF.PI * 0.5f),
            Ease.OutSine => MathF.Sin(t * MathF.PI * 0.5f),
            Ease.InOutSine => -(MathF.Cos(MathF.PI * t) - 1f) * 0.5f,
            Ease.InQuad => t * t,
            Ease.OutQuad => 1f - (1f - t) * (1f - t),
            Ease.InOutQuad => t < 0.5f ? 2f * t * t : 1f - Pow(-2f * t + 2f, 2) * 0.5f,
            Ease.InCubic => t * t * t,
            Ease.OutCubic => 1f - Pow(1f - t, 3),
            Ease.InOutCubic => t < 0.5f ? 4f * t * t * t : 1f - Pow(-2f * t + 2f, 3) * 0.5f,
            Ease.InQuart => t * t * t * t,
            Ease.OutQuart => 1f - Pow(1f - t, 4),
            Ease.InOutQuart => t < 0.5f ? 8f * t * t * t * t : 1f - Pow(-2f * t + 2f, 4) * 0.5f,
            Ease.InQuint => t * t * t * t * t,
            Ease.OutQuint => 1f - Pow(1f - t, 5),
            Ease.InOutQuint => t < 0.5f ? 16f * t * t * t * t * t : 1f - Pow(-2f * t + 2f, 5) * 0.5f,
            Ease.InExpo => t == 0f ? 0f : MathF.Pow(2f, 10f * t - 10f),
            Ease.OutExpo => t == 1f ? 1f : 1f - MathF.Pow(2f, -10f * t),
            Ease.InOutExpo => InOutExpo(t),
            Ease.InCirc => 1f - MathF.Sqrt(1f - t * t),
            Ease.OutCirc => MathF.Sqrt(1f - (t - 1f) * (t - 1f)),
            Ease.InOutCirc => InOutCirc(t),
            Ease.InElastic => InElastic(t),
            Ease.OutElastic => OutElastic(t),
            Ease.InOutElastic => InOutElastic(t),
            Ease.InBack => (BackOvershoot + 1f) * t * t * t - BackOvershoot * t * t,
            Ease.OutBack => 1f + (BackOvershoot + 1f) * Pow(t - 1f, 3) + BackOvershoot * Pow(t - 1f, 2),
            Ease.InOutBack => InOutBack(t),
            Ease.InBounce => 1f - OutBounce(1f - t),
            Ease.OutBounce => OutBounce(t),
            Ease.InOutBounce => t < 0.5f ? (1f - OutBounce(1f - 2f * t)) * 0.5f : (1f + OutBounce(2f * t - 1f)) * 0.5f,
            _ => t
        };
    }

    private static float Pow(float value, int power)
    {
        var result = value;

        for (var i = 1; i < power; i++)
        {
            result *= value;
        }

        return result;
    }

    private static float InOutExpo(float t)
    {
        if (t == 0f || t == 1f)
        {
            return t;
        }

        return t < 0.5f ? MathF.Pow(2f, 20f * t - 10f) * 0.5f : (2f - MathF.Pow(2f, -20f * t + 10f)) * 0.5f;
    }

    private static float InOutCirc(float t)
    {
        return t < 0.5f
            ? (1f - MathF.Sqrt(1f - 4f * t * t)) * 0.5f
            : (MathF.Sqrt(1f - Pow(-2f * t + 2f, 2)) + 1f) * 0.5f;
    }

    private static float InElastic(float t)
    {
        if (t == 0f || t == 1f)
        {
            return t;
        }

        return -MathF.Pow(2f, 10f * t - 10f) * MathF.Sin((t * 10f - 10.75f) * ElasticPeriod);
    }

    private static float OutElastic(float t)
    {
        if (t == 0f || t == 1f)
        {
            return t;
        }

        return MathF.Pow(2f, -10f * t) * MathF.Sin((t * 10f - 0.75f) * ElasticPeriod) + 1f;
    }

    private static float InOutElastic(float t)
    {
        if (t == 0f || t == 1f)
        {
            return t;
        }

        return t < 0.5f
            ? -(MathF.Pow(2f, 20f * t - 10f) * MathF.Sin((20f * t - 11.125f) * ElasticInOutPeriod)) * 0.5f
            : MathF.Pow(2f, -20f * t + 10f) * MathF.Sin((20f * t - 11.125f) * ElasticInOutPeriod) * 0.5f + 1f;
    }

    private static float InOutBack(float t)
    {
        return t < 0.5f
            ? Pow(2f * t, 2) * ((BackInOutOvershoot + 1f) * 2f * t - BackInOutOvershoot) * 0.5f
            : (Pow(2f * t - 2f, 2) * ((BackInOutOvershoot + 1f) * (t * 2f - 2f) + BackInOutOvershoot) + 2f) * 0.5f;
    }

    private static float OutBounce(float t)
    {
        if (t < 1f / BounceDivisor)
        {
            return BounceScale * t * t;
        }

        if (t < 2f / BounceDivisor)
        {
            t -= 1.5f / BounceDivisor;
            return BounceScale * t * t + 0.75f;
        }

        if (t < 2.5f / BounceDivisor)
        {
            t -= 2.25f / BounceDivisor;
            return BounceScale * t * t + 0.9375f;
        }

        t -= 2.625f / BounceDivisor;
        return BounceScale * t * t + 0.984375f;
    }
}
