using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Transform3DExtensions
{
    public static ITween<Vector3> MoveTo(this Transform3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(origin => source.Origin = origin);
        return tween;
    }

    public static ITween<float> MoveXTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionX =>
        {
            var origin = source.Origin;
            origin.X = positionX;
            source.Origin = origin;
        });
        return tween;
    }

    public static ITween<float> MoveYTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionY =>
        {
            var origin = source.Origin;
            origin.Y = positionY;
            source.Origin = origin;
        });
        return tween;
    }

    public static ITween<float> MoveZTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Origin.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(positionZ =>
        {
            var origin = source.Origin;
            origin.Z = positionZ;
            source.Origin = origin;
        });
        return tween;
    }

    public static ITween<Vector3> ScaleTo(this Transform3D source, Vector3 target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Basis.Scale, () => target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(scale =>
        {
            var basis = source.Basis.Scaled(scale / source.Basis.Scale);
            source.Basis = basis;
        });
        return tween;
    }

    public static ITween<float> ScaleXTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Basis.Scale.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(scaleX =>
         {
             var currentScale = source.Basis.Scale;
             var scale = new Vector3(scaleX / currentScale.X, 1, 1);
             var basis = source.Basis.Scaled(scale);
             source.Basis = basis;
         });
        return tween;
    }

    public static ITween<float> ScaleYTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Basis.Scale.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(scaleY =>
        {
            var currentScale = source.Basis.Scale;
            var scale = new Vector3(1, scaleY / currentScale.Y, 1);
            var basis = source.Basis.Scaled(scale);
            source.Basis = basis;
        });
        return tween;
    }

    public static ITween<float> ScaleZTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var tween = context.CreateTween(() => source.Basis.Scale.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(scaleZ =>
         {
             var currentScale = source.Basis.Scale;
             var scale = new Vector3(1, 1, scaleZ / currentScale.Z);
             var basis = source.Basis.Scaled(scale);
             source.Basis = basis;
         });
        return tween;
    }

    public static ITween<Vector3> RotateTo(this Transform3D source, Vector3 targetEuler, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var tween = context.CreateTween(() => startEuler, () => targetEuler, duration, LerpFunctions.Vector3Lerp, autoPlay);
        tween.SetOnUpdate(eulerAngles =>
        {
            var basis = Basis.FromEuler(eulerAngles);
            basis = basis.Scaled(source.Basis.Scale);
            source.Basis = basis;
        });
        return tween;
    }

    public static ITween<float> RotateXTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var tween = context.CreateTween(() => startEuler.X, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotationX =>
        {
            var euler = source.Basis.GetEuler();
            euler.X = rotationX;
            var basis = Basis.FromEuler(euler);
            basis = basis.Scaled(source.Basis.Scale);
            source.Basis = basis;
        });
        return tween;
    }

    public static ITween<float> RotateYTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var tween = context.CreateTween(() => startEuler.Y, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotationY =>
         {
             var euler = source.Basis.GetEuler();
             euler.Y = rotationY;
             var basis = Basis.FromEuler(euler);
             basis = basis.Scaled(source.Basis.Scale);
             source.Basis = basis;
         });
        return tween;
    }

    public static ITween<float> RotateZTo(this Transform3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var tween = context.CreateTween(() => startEuler.Z, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        tween.SetOnUpdate(rotationZ =>
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
