using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Transform3DExtensions
{
    public static ITween MoveTo(this Transform3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        ((ITween<Vector3>)tween).SetOnUpdate(origin => source.Origin = origin);
        return tween;
    }

    public static ITween MoveXTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(positionX =>
        {
            var origin = source.Origin;
            origin.X = positionX;
            source.Origin = origin;
        });
        return tween;
    }

    public static ITween MoveYTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(positionY =>
        {
            var origin = source.Origin;
            origin.Y = positionY;
            source.Origin = origin;
        });
        return tween;
    }

    public static ITween MoveZTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(positionZ =>
        {
            var origin = source.Origin;
            origin.Z = positionZ;
            source.Origin = origin;
        });
        return tween;
    }

    public static ITween ScaleTo(this Transform3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Basis.Scale, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        ((ITween<Vector3>)tween).SetOnUpdate(scale =>
        {
            var basis = source.Basis.Scaled(scale / source.Basis.Scale);
            source.Basis = basis;
        });
        return tween;
    }

    public static ITween ScaleXTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Basis.Scale.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(scaleX =>
         {
             var currentScale = source.Basis.Scale;
             var scale = new Vector3(scaleX / currentScale.X, 1, 1);
             var basis = source.Basis.Scaled(scale);
             source.Basis = basis;
         });
        return tween;
    }

    public static ITween ScaleYTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Basis.Scale.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(scaleY =>
        {
            var currentScale = source.Basis.Scale;
            var scale = new Vector3(1, scaleY / currentScale.Y, 1);
            var basis = source.Basis.Scaled(scale);
            source.Basis = basis;
        });
        return tween;
    }

    public static ITween ScaleZTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Basis.Scale.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(scaleZ =>
         {
             var currentScale = source.Basis.Scale;
             var scale = new Vector3(1, 1, scaleZ / currentScale.Z);
             var basis = source.Basis.Scaled(scale);
             source.Basis = basis;
         });
        return tween;
    }

    public static ITween RotateTo(this Transform3D source, Vector3 targetEuler, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var tween = context.CreateTween(() => startEuler, () => targetEuler, duration, LerpFunctions.Vector3Lerp, autoPlay);
        ((ITween<Vector3>)tween).SetOnUpdate(eulerAngles =>
        {
            var basis = Basis.FromEuler(eulerAngles);
            basis = basis.Scaled(source.Basis.Scale);
            source.Basis = basis;
        });
        return tween;
    }

    public static ITween RotateXTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var tween = context.CreateTween(() => startEuler.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(rotationX =>
        {
            var euler = source.Basis.GetEuler();
            euler.X = rotationX;
            var basis = Basis.FromEuler(euler);
            basis = basis.Scaled(source.Basis.Scale);
            source.Basis = basis;
        });
        return tween;
    }

    public static ITween RotateYTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var tween = context.CreateTween(() => startEuler.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(rotationY =>
         {
             var euler = source.Basis.GetEuler();
             euler.Y = rotationY;
             var basis = Basis.FromEuler(euler);
             basis = basis.Scaled(source.Basis.Scale);
             source.Basis = basis;
         });
        return tween;
    }

    public static ITween RotateZTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var tween = context.CreateTween(() => startEuler.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(rotationZ =>
         {
             var euler = source.Basis.GetEuler();
             euler.Z = rotationZ;
             var basis = Basis.FromEuler(euler);
             basis = basis.Scaled(source.Basis.Scale);
             source.Basis = basis;
         });
        return tween;
    }
}
