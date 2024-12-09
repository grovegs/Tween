using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Node2DExtensions
{
    public static ITween MoveTo(this Node2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GlobalPosition, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(position => source.GlobalPosition = position);
        return tween;
    }

    public static ITween MoveLocalTo(this Node2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(position => source.Position = position);
        return tween;
    }

    public static ITween MoveXTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween MoveLocalXTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionX =>
        {
            var localPosition = source.Position;
            localPosition.X = positionX;
            source.Position = localPosition;
        });
        return tween;
    }

    public static ITween MoveYTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween MoveLocalYTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Position.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionY =>
        {
            var localPosition = source.Position;
            localPosition.Y = positionY;
            source.Position = localPosition;
        });
        return tween;
    }

    public static ITween RotateTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.GlobalRotation, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotation => source.GlobalRotation = Mathf.DegToRad(rotation));
        return tween;
    }

    public static ITween RotateLocalTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Rotation, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotation => source.Rotation = Mathf.DegToRad(rotation));
        return tween;
    }

    public static ITween ScaleTo(this Node2D source, Vector2 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Scale, () => target, duration, LerpFunctions.Vector2Lerp, autoPlay);
        tween.SetOnUpdate(scale => source.Scale = scale);
        return tween;
    }

    public static ITween ScaleXTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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

    public static ITween ScaleYTo(this Node2D source, float target, float duration, TweenerContext context, bool autoPlay = true)
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
}
