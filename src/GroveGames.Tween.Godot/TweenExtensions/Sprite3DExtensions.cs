using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Sprite3DExtensions
{
    public static TweenBuilder FadeTo(this Sprite3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var alpha = source.Modulate.A;
        var builder = context.CreateTween(alpha, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(newAlpha =>
        {
            var modulate = source.Modulate;
            modulate.A = target;
            source.Modulate = modulate;
        });
        return builder;
    }
}
