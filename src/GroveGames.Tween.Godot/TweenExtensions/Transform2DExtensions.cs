using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Transform2DExtensions
{
    public static ITween MoveTo(this Transform2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        ((ITween<Vector2>)tween).SetOnUpdate(position =>
         {
             var transform = source;
             transform.Origin = position;
             source = transform;
         });
        return tween;
    }

    public static ITween MoveXTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(positionX =>
        {
            var transform = source;
            var origin = transform.Origin;
            origin.X = positionX;
            transform.Origin = origin;
            source = transform;
        });
        return tween;
    }

    public static ITween MoveYTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(positionY =>
        {
            var transform = source;
            var origin = transform.Origin;
            origin.Y = positionY;
            transform.Origin = origin;
            source = transform;
        });
        return tween;
    }

    public static ITween RotateTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Rotation, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(rotation =>
        {
            var transform = source;
            transform = transform.Rotated(Mathf.DegToRad(rotation) - transform.Rotation);
            source = transform;
        });
        return tween;
    }

    public static ITween ScaleTo(this Transform2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        ((ITween<Vector2>)tween).SetOnUpdate(scale =>
        {
            var transform = source;
            transform = transform.Scaled(scale / transform.Scale);
            source = transform;
        });
        return tween;
    }

    public static ITween ScaleXTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(scaleX =>
        {
            var transform = source;
            var currentScale = transform.Scale;
            var scale = new Vector2(scaleX / currentScale.X, 1);
            transform = transform.Scaled(scale);
            source = transform;
        });
        return tween;
    }

    public static ITween ScaleYTo(this Transform2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(scaleY =>
        {
            var transform = source;
            var currentScale = transform.Scale;
            var scale = new Vector2(1, scaleY / currentScale.Y);
            transform = transform.Scaled(scale);
            source = transform;
        });
        return tween;
    }
}
