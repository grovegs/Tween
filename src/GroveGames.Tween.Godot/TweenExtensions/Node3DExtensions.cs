using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Node3DExtensions
{
    public static TweenBuilder MoveTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(position => source.GlobalPosition = position);
        return builder;
    }

    public static TweenBuilder MoveXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionX =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.X = positionX;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static TweenBuilder MoveYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionY =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.Y = positionY;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static TweenBuilder MoveZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionZ =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.Z = positionZ;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static TweenBuilder MoveLocalTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(position => source.Position = position);
        return builder;
    }

    public static TweenBuilder MoveLocalXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionX =>
        {
            var position = source.Position;
            position.X = positionX;
            source.Position = position;
        });
        return builder;
    }

    public static TweenBuilder MoveLocalYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionY =>
        {
            var position = source.Position;
            position.Y = positionY;
            source.Position = position;
        });
        return builder;
    }

    public static TweenBuilder MoveLocalZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionZ =>
        {
            var position = source.Position;
            position.Z = positionZ;
            source.Position = position;
        });
        return builder;
    }

    public static TweenBuilder RotateTo(this Node3D source, Vector3 targetDegrees, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var builder = context.CreateTween(startDegrees, targetDegrees, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(eulerAngles => source.RotationDegrees = eulerAngles);
        return builder;
    }

    public static TweenBuilder RotateXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var builder = context.CreateTween(startDegrees.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationX =>
        {
            var euler = source.RotationDegrees;
            euler.X = rotationX;
            source.RotationDegrees = euler;
        });
        return builder;
    }

    public static TweenBuilder RotateYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var builder = context.CreateTween(startDegrees.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationY =>
        {
            var euler = source.RotationDegrees;
            euler.Y = rotationY;
            source.RotationDegrees = euler;
        });
        return builder;
    }

    public static TweenBuilder RotateZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var builder = context.CreateTween(startDegrees.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationZ =>
        {
            var euler = source.RotationDegrees;
            euler.Z = rotationZ;
            source.RotationDegrees = euler;
        });
        return builder;
    }

    public static TweenBuilder ScaleTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(scale => source.Scale = scale);
        return builder;
    }

    public static TweenBuilder ScaleXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleX =>
        {
            var scale = source.Scale;
            scale.X = scaleX;
            source.Scale = scale;
        });
        return builder;
    }

    public static TweenBuilder ScaleYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleY =>
        {
            var scale = source.Scale;
            scale.Y = scaleY;
            source.Scale = scale;
        });
        return builder;
    }

    public static TweenBuilder ScaleZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleZ =>
        {
            var scale = source.Scale;
            scale.Z = scaleZ;
            source.Scale = scale;
        });
        return builder;
    }
}
