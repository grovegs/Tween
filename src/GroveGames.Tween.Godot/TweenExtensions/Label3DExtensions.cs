using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Label3DExtensions
{
    public static TweenBuilder FadeTo(this Label3D source, float duration, float target, TweenerContext context, bool autoPlay = true)
    {
        var alpha = source.Modulate.A;
        var builder = context.Create(alpha, target, duration, LerpFunctions.FloatLerp, autoPlay);
        builder.OnUpdate<float>(newAlpha =>
        {
            var modulate = source.Modulate;
            modulate.A = target;
            source.Modulate = modulate;
        });
        return builder;
    }
}
