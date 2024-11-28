using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Node3DExtensions
{
    public static TweenBuilder MoveTo(this Node3D source, float duration, Vector3 target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.GlobalPosition, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(position => source.GlobalPosition = position);
        return builder;
    }

    public static TweenBuilder MoveXTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.GlobalPosition.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionX =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.X = positionX;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static TweenBuilder MoveYTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.GlobalPosition.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionY =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.Y = positionY;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static TweenBuilder MoveZTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.GlobalPosition.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionZ =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.Z = positionZ;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static TweenBuilder RotateTo(this Node3D source, float duration, Vector3 targetEuler, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Rotation;
        var builder = context.Create(startEuler, targetEuler, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(eulerAngles => source.Rotation = eulerAngles);
        return builder;
    }

    public static TweenBuilder RotateXTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Rotation;
        var builder = context.Create(startEuler.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationX =>
        {
            var euler = source.Rotation;
            euler.X = rotationX;
            source.Rotation = euler;
        });
        return builder;
    }

    public static TweenBuilder RotateYTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Rotation;
        var builder = context.Create(startEuler.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationY =>
        {
            var euler = source.Rotation;
            euler.Y = rotationY;
            source.Rotation = euler;
        });
        return builder;
    }

    public static TweenBuilder RotateZTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Rotation;
        var builder = context.Create(startEuler.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationZ =>
        {
            var euler = source.Rotation;
            euler.Z = rotationZ;
            source.Rotation = euler;
        });
        return builder;
    }

    public static TweenBuilder ScaleTo(this Node3D source, float duration, Vector3 target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Scale, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(scale => source.Scale = scale);
        return builder;
    }

    public static TweenBuilder ScaleXTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Scale.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleX =>
        {
            var scale = source.Scale;
            scale.X = scaleX;
            source.Scale = scale;
        });
        return builder;
    }

    public static TweenBuilder ScaleYTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Scale.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleY =>
        {
            var scale = source.Scale;
            scale.Y = scaleY;
            source.Scale = scale;
        });
        return builder;
    }

    public static TweenBuilder ScaleZTo(this Node3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Scale.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleZ =>
        {
            var scale = source.Scale;
            scale.Z = scaleZ;
            source.Scale = scale;
        });
        return builder;
    }
}
