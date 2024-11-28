using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Transform3DExtensions
{
    public static TweenBuilder MoveTo(this Transform3D source, float duration, Vector3 target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Origin, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(origin => source.Origin = origin);
        return builder;
    }

    public static TweenBuilder MoveXTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Origin.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionX =>
        {
            var origin = source.Origin;
            origin.X = positionX;
            source.Origin = origin;
        });
        return builder;
    }

    public static TweenBuilder MoveYTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Origin.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionY =>
        {
            var origin = source.Origin;
            origin.Y = positionY;
            source.Origin = origin;
        });
        return builder;
    }

    public static TweenBuilder MoveZTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Origin.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(positionZ =>
        {
            var origin = source.Origin;
            origin.Z = positionZ;
            source.Origin = origin;
        });
        return builder;
    }

    public static TweenBuilder ScaleTo(this Transform3D source, float duration, Vector3 target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Basis.Scale, target, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(scale =>
        {
            var basis = source.Basis.Scaled(scale / source.Basis.Scale); // Adjust scaling
            source.Basis = basis;
        });
        return builder;
    }

    public static TweenBuilder ScaleXTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Basis.Scale.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleX =>
        {
            var currentScale = source.Basis.Scale;
            var scale = new Vector3(scaleX / currentScale.X, 1, 1); // Adjust X-axis scale only
            var basis = source.Basis.Scaled(scale);
            source.Basis = basis;
        });
        return builder;
    }

    public static TweenBuilder ScaleYTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Basis.Scale.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleY =>
        {
            var currentScale = source.Basis.Scale;
            var scale = new Vector3(1, scaleY / currentScale.Y, 1); // Adjust Y-axis scale only
            var basis = source.Basis.Scaled(scale);
            source.Basis = basis;
        });
        return builder;
    }

    public static TweenBuilder ScaleZTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var builder = context.Create(source.Basis.Scale.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(scaleZ =>
        {
            var currentScale = source.Basis.Scale;
            var scale = new Vector3(1, 1, scaleZ / currentScale.Z); // Adjust Z-axis scale only
            var basis = source.Basis.Scaled(scale);
            source.Basis = basis;
        });
        return builder;
    }

    public static TweenBuilder RotateTo(this Transform3D source, float duration, Vector3 targetEuler, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var builder = context.Create(startEuler, targetEuler, duration, LerpFunctions.Vector3Lerp, autoPlay);
        builder.OnUpdate<Vector3>(eulerAngles =>
        {
            var basis = Basis.FromEuler(eulerAngles); // Create Basis from Euler angles
            basis = basis.Scaled(source.Basis.Scale); // Preserve scale
            source.Basis = basis;
        });
        return builder;
    }

    public static TweenBuilder RotateXTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var builder = context.Create(startEuler.X, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationX =>
        {
            var euler = source.Basis.GetEuler();
            euler.X = rotationX;
            var basis = Basis.FromEuler(euler);
            basis = basis.Scaled(source.Basis.Scale); // Preserve scale
            source.Basis = basis;
        });
        return builder;
    }

    public static TweenBuilder RotateYTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var builder = context.Create(startEuler.Y, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationY =>
        {
            var euler = source.Basis.GetEuler();
            euler.Y = rotationY;
            var basis = Basis.FromEuler(euler);
            basis = basis.Scaled(source.Basis.Scale); // Preserve scale
            source.Basis = basis;
        });
        return builder;
    }

    public static TweenBuilder RotateZTo(this Transform3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var startEuler = source.Basis.GetEuler();
        var builder = context.Create(startEuler.Z, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(rotationZ =>
        {
            var euler = source.Basis.GetEuler();
            euler.Z = rotationZ;
            var basis = Basis.FromEuler(euler);
            basis = basis.Scaled(source.Basis.Scale); // Preserve scale
            source.Basis = basis;
        });
        return builder;
    }
}
