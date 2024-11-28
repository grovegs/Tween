using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Node2DExtensions
{
    // Move the global position
    public static TweenBuilder MoveTo(this Node2D source, float duration, Vector2 target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.GlobalPosition, target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        builder.OnUpdate<Vector2>(position => source.GlobalPosition = position);
        return builder;
    }

    // Move the local position
    public static TweenBuilder MoveLocalTo(this Node2D source, float duration, Vector2 target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Position, target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        builder.OnUpdate<Vector2>(position => source.Position = position);
        return builder;
    }

    // Move X position globally
    public static TweenBuilder MoveXTo(this Node2D source, float duration, float target, TweenerContext context, bool autoPlay = true)
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

    // Move X position locally
    public static TweenBuilder MoveLocalXTo(this Node2D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Position.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionX =>
        {
            var localPosition = source.Position;
            localPosition.X = positionX;
            source.Position = localPosition;
        });
        return builder;
    }

    // Move Y position globally
    public static TweenBuilder MoveYTo(this Node2D source, float duration, float target, TweenerContext context, bool autoPlay = true)
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

    // Move Y position locally
    public static TweenBuilder MoveLocalYTo(this Node2D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Position.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionY =>
        {
            var localPosition = source.Position;
            localPosition.Y = positionY;
            source.Position = localPosition;
        });
        return builder;
    }

    public static TweenBuilder RotateTo(this Node2D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.GlobalRotation, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotation => source.GlobalRotation = rotation);
        return builder;
    }

    public static TweenBuilder RotateLocalTo(this Node2D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Rotation, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotation => source.Rotation = rotation);
        return builder;
    }

    public static TweenBuilder ScaleTo(this Node2D source, float duration, Vector2 target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Scale, target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        builder.OnUpdate<Vector2>(scale => source.Scale = scale);
        return builder;
    }

    public static TweenBuilder ScaleXTo(this Node2D source, float duration, float target, TweenerContext context, bool autoPlay = true)
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

    // Scale Y axis
    public static TweenBuilder ScaleYTo(this Node2D source, float duration, float target, TweenerContext context, bool autoPlay = true)
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
}
