using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Node3DExtensions
{
    public static ITween MoveTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        ((ITween<Vector3>)builder).SetOnUpdate(position => source.GlobalPosition = position);
        return builder;
    }

    public static ITween MoveXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(positionX =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.X = positionX;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static ITween MoveYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
       ((ITween<float>)builder).SetOnUpdate(positionY =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.Y = positionY;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static ITween MoveZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(positionZ =>
        {
            var globalPosition = source.GlobalPosition;
            globalPosition.Z = positionZ;
            source.GlobalPosition = globalPosition;
        });
        return builder;
    }

    public static ITween MoveLocalTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        ((ITween<Vector3>)builder).SetOnUpdate(position => source.Position = position);
        return builder;
    }

    public static ITween MoveLocalXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(positionX =>
        {
            var position = source.Position;
            position.X = positionX;
            source.Position = position;
        });
        return builder;
    }

    public static ITween MoveLocalYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(positionY =>
        {
            var position = source.Position;
            position.Y = positionY;
            source.Position = position;
        });
        return builder;
    }

    public static ITween MoveLocalZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
       ((ITween<float>)builder).SetOnUpdate(positionZ =>
        {
            var position = source.Position;
            position.Z = positionZ;
            source.Position = position;
        });
        return builder;
    }

    public static ITween RotateTo(this Node3D source, Vector3 targetDegrees, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var builder = context.CreateTween(startDegrees, targetDegrees, duration, LerpFunctions.Vector3Lerp, autoPlay);
        ((ITween<Vector3>)builder).SetOnUpdate(eulerAngles => source.RotationDegrees = eulerAngles);
        return builder;
    }

    public static ITween RotateXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var builder = context.CreateTween(startDegrees.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(rotationX =>
        {
            var euler = source.RotationDegrees;
            euler.X = rotationX;
            source.RotationDegrees = euler;
        });
        return builder;
    }

    public static ITween RotateYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var builder = context.CreateTween(startDegrees.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(rotationY =>
        {
            var euler = source.RotationDegrees;
            euler.Y = rotationY;
            source.RotationDegrees = euler;
        });
        return builder;
    }

    public static ITween RotateZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startDegrees = source.RotationDegrees;
        var builder = context.CreateTween(startDegrees.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(rotationZ =>
        {
            var euler = source.RotationDegrees;
            euler.Z = rotationZ;
            source.RotationDegrees = euler;
        });
        return builder;
    }

    public static ITween ScaleTo(this Node3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        ((ITween<Vector3>)builder).SetOnUpdate(scale => source.Scale = scale);
        return builder;
    }

    public static ITween ScaleXTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(scaleX =>
        {
            var scale = source.Scale;
            scale.X = scaleX;
            source.Scale = scale;
        });
        return builder;
    }

    public static ITween ScaleYTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(scaleY =>
        {
            var scale = source.Scale;
            scale.Y = scaleY;
            source.Scale = scale;
        });
        return builder;
    }

    public static ITween ScaleZTo(this Node3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(scaleZ =>
        {
            var scale = source.Scale;
            scale.Z = scaleZ;
            source.Scale = scale;
        });
        return builder;
    }
}
