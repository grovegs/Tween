using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class ControlExtensions
{
    public static ITween MoveTo(this Control source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GlobalPosition, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(position => source.GlobalPosition = position);
        return tween;
    }

    public static ITween MoveXTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GlobalPosition.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionX =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.X = positionX;
            source.GlobalPosition = globalPosition;
        });
        return tween;
    }

    public static ITween MoveYTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GlobalPosition.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionY =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.Y = positionY;
            source.GlobalPosition = globalPosition;
        });
        return tween;
    }

    public static ITween MoveLocalTo(this Control source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(position => source.Position = position);
        return tween;
    }

    public static ITween MoveLocalXTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionX =>
        {
            var position = source.Position;
            position.X = positionX;
            source.Position = position;
        });
        return tween;
    }

    public static ITween MoveLocalYTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionY =>
        {
            var position = source.Position;
            position.Y = positionY;
            source.Position = position;
        });
        return tween;
    }

    public static ITween SizeTo(this Control source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Size, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(size => source.Size = size);
        return tween;
    }

    public static ITween SizeWidthTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Size.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(width =>
        {
            var size = source.Size;
            size.X = width;
            source.Size = size;
        });
        return tween;
    }

    public static ITween SizeHeightTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Size.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(height =>
        {
            var size = source.Size;
            size.Y = height;
            source.Size = size;
        });
        return tween;
    }

    public static ITween MinSizeTo(this Control source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.CustomMinimumSize, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(size => source.CustomMinimumSize = size);
        return tween;
    }

    public static ITween MinSizeWidthTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.CustomMinimumSize.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(width =>
        {
            var size = source.CustomMinimumSize;
            size.X = width;
            source.CustomMinimumSize = size;
        });
        return tween;
    }

    public static ITween MinSizeHeightTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.CustomMinimumSize.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(height =>
        {
            var size = source.CustomMinimumSize;
            size.Y = height;
            source.CustomMinimumSize = size;
        });
        return tween;
    }

    public static ITween RotateTo(this Control source, float targetRadians, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Rotation, () => targetRadians, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotation => source.Rotation = rotation);
        return tween;
    }

    public static ITween RotateDegreesTo(this Control source, float targetDegrees, float duration, TweenerContext context, bool autoPlay = true)
    {
        var targetRadians = Mathf.DegToRad(targetDegrees);
        return RotateTo(source, targetRadians, duration, context, autoPlay);
    }

    public static ITween ScaleTo(this Control source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(scale => source.Scale = scale);
        return tween;
    }

    public static ITween ScaleXTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(scaleX =>
        {
            var scale = source.Scale;
            scale.X = scaleX;
            source.Scale = scale;
        });
        return tween;
    }

    public static ITween ScaleYTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(scaleY =>
        {
            var scale = source.Scale;
            scale.Y = scaleY;
            source.Scale = scale;
        });
        return tween;
    }

    public static ITween PivotTo(this Control source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.PivotOffset, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(pivot => source.PivotOffset = pivot);
        return tween;
    }

    public static ITween ModulateTo(this Control source, Color target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Modulate, () => target, duration, LerpFunctions.ColorLerp, autoPlay);
        tween.SetOnUpdate(color => source.Modulate = color);
        return tween;
    }

    public static ITween SelfModulateTo(this Control source, Color target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.SelfModulate, () => target, duration, LerpFunctions.ColorLerp, autoPlay);
        tween.SetOnUpdate(color => source.SelfModulate = color);
        return tween;
    }

    public static ITween FadeTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Modulate.A, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(alpha =>
        {
            var color = source.Modulate;
            color.A = alpha;
            source.Modulate = color;
        });
        return tween;
    }

    public static ITween SelfFadeTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.SelfModulate.A, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(alpha =>
        {
            var color = source.SelfModulate;
            color.A = alpha;
            source.SelfModulate = color;
        });
        return tween;
    }

    public static ITween MarginLeftTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GetThemeConstant("margin_left"), () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(margin =>
        {
            source.AddThemeConstantOverride("margin_left", (int)margin);
        });
        return tween;
    }

    public static ITween MarginRightTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GetThemeConstant("margin_right"), () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(margin =>
        {
            source.AddThemeConstantOverride("margin_right", (int)margin);
        });
        return tween;
    }

    public static ITween MarginTopTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GetThemeConstant("margin_top"), () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(margin =>
        {
            source.AddThemeConstantOverride("margin_top", (int)margin);
        });
        return tween;
    }

    public static ITween MarginBottomTo(this Control source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GetThemeConstant("margin_bottom"), () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(margin =>
        {
            source.AddThemeConstantOverride("margin_bottom", (int)margin);
        });
        return tween;
    }

    public static ITween ShakePosition(
        this Control source,
        Vector2 strength,
        float duration,
        TweenerContext context,
        int oscillations = 10,
        bool autoPlay = true)
    {
        var originalPosition = source.GlobalPosition;
        var tween = context.CreateTween(
            () => 0f,
            () => 1f,
            duration,
            LerpFunctions.FloatLerp,
            autoPlay);

        tween.SetOnUpdate(progress =>
        {
            var noiseX = Mathf.Sin(progress * Mathf.Pi * oscillations) * strength.X * (1f - progress);
            var noiseY = Mathf.Sin(progress * Mathf.Pi * oscillations * 1.1f) * strength.Y * (1f - progress);

            source.GlobalPosition = originalPosition + new Vector2(noiseX, noiseY);
        });

        tween.SetOnComplete(() =>
        {
            source.GlobalPosition = originalPosition;
        });

        return tween;
    }

    public static ITween ShakeRotation(
        this Control source,
        float strength,
        float duration,
        TweenerContext context,
        int oscillations = 10,
        bool autoPlay = true)
    {
        var originalRotation = source.Rotation;
        var tween = context.CreateTween(
            () => 0f,
            () => 1f,
            duration,
            LerpFunctions.FloatLerp,
            autoPlay);

        tween.SetOnUpdate(progress =>
        {
            var noise = Mathf.Sin(progress * Mathf.Pi * oscillations) * strength * (1f - progress);
            source.Rotation = originalRotation + noise;
        });

        tween.SetOnComplete(() =>
        {
            source.Rotation = originalRotation;
        });

        return tween;
    }

    public static ITween ShakeScale(
        this Control source,
        Vector2 strength,
        float duration,
        TweenerContext context,
        int oscillations = 10,
        bool autoPlay = true)
    {
        var originalScale = source.Scale;
        var tween = context.CreateTween(
            () => 0f,
            () => 1f,
            duration,
            LerpFunctions.FloatLerp,
            autoPlay);

        tween.SetOnUpdate(progress =>
        {
            var noiseX = Mathf.Sin(progress * Mathf.Pi * oscillations) * strength.X * (1f - progress);
            var noiseY = Mathf.Sin(progress * Mathf.Pi * oscillations * 1.1f) * strength.Y * (1f - progress);

            source.Scale = originalScale + new Vector2(noiseX, noiseY);
        });

        tween.SetOnComplete(() =>
        {
            source.Scale = originalScale;
        });

        return tween;
    }

    public static ITween PunchPosition(
        this Control source,
        Vector2 punchStrength,
        float duration,
        TweenerContext context,
        bool autoPlay = true)
    {
        var originalPosition = source.GlobalPosition;

        var tween = context.CreateTween(
            () => 0f,
            () => 1f,
            duration,
            LerpFunctions.FloatLerp,
            autoPlay);

        tween.SetOnUpdate(progress =>
        {
            var easeOutElastic = Mathf.Pow(2, -10 * progress) * Mathf.Sin((progress - 0.075f) * (2 * Mathf.Pi) / 0.3f) + 1;
            source.GlobalPosition = originalPosition + punchStrength * easeOutElastic;
        });

        tween.SetOnComplete(() =>
        {
            source.GlobalPosition = originalPosition;
        });

        return tween;
    }

    public static ITween PunchRotation(
        this Control source,
        float punchStrength,
        float duration,
        TweenerContext context,
        bool autoPlay = true)
    {
        var originalRotation = source.Rotation;

        var tween = context.CreateTween(
            () => 0f,
            () => 1f,
            duration,
            LerpFunctions.FloatLerp,
            autoPlay);

        tween.SetOnUpdate(progress =>
        {
            var easeOutElastic = Mathf.Pow(2, -10 * progress) * Mathf.Sin((progress - 0.075f) * (2 * Mathf.Pi) / 0.3f) + 1;
            source.Rotation = originalRotation + punchStrength * easeOutElastic;
        });

        tween.SetOnComplete(() =>
        {
            source.Rotation = originalRotation;
        });

        return tween;
    }

    public static ITween PunchScale(
        this Control source,
        Vector2 punchStrength,
        float duration,
        TweenerContext context,
        bool autoPlay = true)
    {
        var originalScale = source.Scale;

        var tween = context.CreateTween(
            () => 0f,
            () => 1f,
            duration,
            LerpFunctions.FloatLerp,
            autoPlay);

        tween.SetOnUpdate(progress =>
        {
            var easeOutElastic = Mathf.Pow(2, -10 * progress) * Mathf.Sin((progress - 0.075f) * (2 * Mathf.Pi) / 0.3f) + 1;
            source.Scale = originalScale + punchStrength * easeOutElastic;
        });

        tween.SetOnComplete(() =>
        {
            source.Scale = originalScale;
        });

        return tween;
    }
}