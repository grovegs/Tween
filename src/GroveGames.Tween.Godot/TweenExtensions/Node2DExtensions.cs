using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Node2DExtensions
{
    /*
    public static ITween MoveTo(this Node2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalPosition, target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        ((ITween<Vector2>)builder).SetOnUpdate(position => source.GlobalPosition = position);
        return builder;
    }

    public static ITween MoveLocalTo(this Node2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position, target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        ((ITween<Vector2>)builder).SetOnUpdate(position => source.Position = position);
        return builder;
    }

    public static ITween MoveXTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween MoveLocalXTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(positionX =>
        {
            var localPosition = source.Position;
            localPosition.X = positionX;
            source.Position = localPosition;
        });
        return builder;
    }

    public static ITween MoveYTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween MoveLocalYTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Position.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(positionY =>
        {
            var localPosition = source.Position;
            localPosition.Y = positionY;
            source.Position = localPosition;
        });
        return builder;
    }

    public static ITween RotateTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.GlobalRotation, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(rotation => source.GlobalRotation = Mathf.DegToRad(rotation));
        return builder;
    }

    public static ITween RotateLocalTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Rotation, target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)builder).SetOnUpdate(rotation => source.Rotation = Mathf.DegToRad(rotation));
        return builder;
    }

    public static ITween ScaleTo(this Node2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale, target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        ((ITween<Vector2>)builder).SetOnUpdate(scale => source.Scale = scale);
        return builder;
    }

    public static ITween ScaleXTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween ScaleYTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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
    */
}
