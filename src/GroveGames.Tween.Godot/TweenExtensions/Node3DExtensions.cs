using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Node3DExtensions
{

    public static ITween<Vector3> MoveTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GlobalPosition, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(position => source.GlobalPosition = position);
        return tween;
    }

    public static ITween<float> MoveXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<float> MoveYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<float> MoveZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<Vector3> MoveLocalTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(position => source.Position = position);
        return tween;
    }

    public static ITween<float> MoveLocalXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<float> MoveLocalYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<float> MoveLocalZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<Vector3> RotateTo(this Node3D source, Vector3 targetDegrees, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var tween = context.CreateTween(() => startDegrees, () => targetDegrees, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(eulerAngles => source.RotationDegrees = eulerAngles);
        return tween;
    }

    public static ITween<float> RotateXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<float> RotateYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<float> RotateZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<Vector3> ScaleTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(scale => source.Scale = scale);
        return tween;
    }

    public static ITween<float> ScaleXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<float> ScaleYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween<float> ScaleZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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
}
