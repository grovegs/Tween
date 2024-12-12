using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Node3DExtensions
{

    public static ITween MoveTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GlobalPosition, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(position => source.GlobalPosition = position);
        return tween;
    }

    public static ITween MoveXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(() => source.GlobalPosition.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.SetOnUpdate(positionX =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.X = positionX;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static ITween MoveYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween MoveZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GlobalPosition.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionZ =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.Z = positionZ;
            source.GlobalPosition = globalPosition;
        });
        return tween;
    }

    public static ITween MoveLocalTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(position => source.Position = position);
        return tween;
    }

    public static ITween MoveLocalXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween MoveLocalYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween MoveLocalZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionZ =>
         {
             var position = source.Position;
             position.Z = positionZ;
             source.Position = position;
         });
        return tween;
    }

    public static ITween RotateTo(this Node3D source, Vector3 targetDegrees, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var tween = context.CreateTween(() => startDegrees, () => targetDegrees, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(eulerAngles => source.RotationDegrees = eulerAngles);
        return tween;
    }

    public static ITween RotateXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var tween = context.CreateTween(() => startDegrees.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotationX =>
        {
            var euler = source.RotationDegrees;
            euler.X = rotationX;
            source.RotationDegrees = euler;
        });
        return tween;
    }

    public static ITween RotateYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var tween = context.CreateTween(() => startDegrees.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotationY =>
        {
            var euler = source.RotationDegrees;
            euler.Y = rotationY;
            source.RotationDegrees = euler;
        });
        return tween;
    }

    public static ITween RotateZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var tween = context.CreateTween(() => startDegrees.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotationZ =>
        {
            var euler = source.RotationDegrees;
            euler.Z = rotationZ;
            source.RotationDegrees = euler;
        });
        return tween;
    }

    public static ITween ScaleTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(scale => source.Scale = scale);
        return tween;
    }

    public static ITween ScaleXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween ScaleYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween ScaleZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(scaleZ =>
        {
            var scale = source.Scale;
            scale.Z = scaleZ;
            source.Scale = scale;
        });
        return tween;
    }

    public static ITween ShakePosition(
    this Node3D source,
    Vector3 strength,
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
            float noiseX = Mathf.Sin(progress * Mathf.Pi * oscillations) * strength.X * (1f - progress);
            float noiseY = Mathf.Sin(progress * Mathf.Pi * oscillations * 1.1f) * strength.Y * (1f - progress);
            float noiseZ = Mathf.Sin(progress * Mathf.Pi * oscillations * 1.2f) * strength.Z * (1f - progress);

            source.GlobalPosition = originalPosition + new Vector3(noiseX, noiseY, noiseZ);
        });

        tween.SetOnComplete(() =>
        {
            source.GlobalPosition = originalPosition;
        });

        return tween;
    }

    public static ITween ShakeRotation(
        this Node3D source,
        Vector3 strength,
        float duration,
        TweenerContext context,
        int oscillations = 10,
        bool autoPlay = true)
    {
        var originalRotation = source.RotationDegrees;
        var tween = context.CreateTween(
            () => 0f,
            () => 1f,
            duration,
            LerpFunctions.FloatLerp,
            autoPlay);

        tween.SetOnUpdate(progress =>
        {
            float noiseX = Mathf.Sin(progress * Mathf.Pi * oscillations) * strength.X * (1f - progress);
            float noiseY = Mathf.Sin(progress * Mathf.Pi * oscillations * 1.1f) * strength.Y * (1f - progress);
            float noiseZ = Mathf.Sin(progress * Mathf.Pi * oscillations * 1.2f) * strength.Z * (1f - progress);

            source.RotationDegrees = originalRotation + new Vector3(noiseX, noiseY, noiseZ);
        });

        tween.SetOnComplete(() =>
        {
            source.RotationDegrees = originalRotation;
        });

        return tween;
    }
}
