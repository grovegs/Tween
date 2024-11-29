using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Transform2DExtensions
{
    public static TweenBuilder MoveTo(this Transform2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Origin, target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        builder.OnUpdate<Vector2>(position =>
        {
            var transform = source;
            transform.Origin = position;
            source = transform;
        });
        return builder;
    }

    public static TweenBuilder MoveXTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Origin.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionX =>
        {
            var transform = source;
            var origin = transform.Origin;
            origin.X = positionX;
            transform.Origin = origin;
            source = transform;
        });
        return builder;
    }

    public static TweenBuilder MoveYTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Origin.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionY =>
        {
            var transform = source;
            var origin = transform.Origin;
            origin.Y = positionY;
            transform.Origin = origin;
            source = transform;
        });
        return builder;
    }

    public static TweenBuilder RotateTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Rotation, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotation =>
        {
            var transform = source;
            transform = transform.Rotated(Mathf.DegToRad(rotation) - transform.Rotation);
            source = transform;
        });
        return builder;
    }

    public static TweenBuilder ScaleTo(this Transform2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale, target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        builder.OnUpdate<Vector2>(scale =>
        {
            var transform = source;
            transform = transform.Scaled(scale / transform.Scale);
            source = transform;
        });
        return builder;
    }

    public static TweenBuilder ScaleXTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleX =>
        {
            var transform = source;
            var currentScale = transform.Scale;
            var scale = new Vector2(scaleX / currentScale.X, 1);
            transform = transform.Scaled(scale);
            source = transform;
        });
        return builder;
    }

    public static TweenBuilder ScaleYTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.CreateTween(source.Scale.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleY =>
        {
            var transform = source;
            var currentScale = transform.Scale;
            var scale = new Vector2(1, scaleY / currentScale.Y);
            transform = transform.Scaled(scale);
            source = transform;
        });
        return builder;
    }
}
