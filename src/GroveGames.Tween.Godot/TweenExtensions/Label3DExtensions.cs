using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Lerp;

namespace GroveGames.Tween.TweenExtensions;

public static class Label3DExtensions
{
    public static ITween FadeTo(this Label3D source, float target, float duration, TweenerContext context, bool autoPlay = true)
    {
        var alpha = source.Modulate.A;
        var tween = context.CreateTween(() => alpha, () => target, duration, LerpFunctions.FloatLerp, autoPlay);
        ((ITween<float>)tween).SetOnUpdate(newAlpha =>
        {
            var modulate = source.Modulate;
            modulate.A = target;
            source.Modulate = modulate;
        });
        return tween;
    }
}
