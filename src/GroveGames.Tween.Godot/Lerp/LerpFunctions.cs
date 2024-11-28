using System;

using Godot;

namespace GroveGames.Tween.Lerp;

public static class LerpFunctions
{

    public static float FloatLerp(float start, float end, float t)
    {
        return start + (end - start) * t;
    }

    public static Vector2 Vector2Lerp(Vector2 start, Vector2 end, float t)
    {
        return new Vector2(
            FloatLerp(start.X, end.X, t),
            FloatLerp(start.Y, end.Y, t)
        );
    }

    public static Vector3 Vector3Lerp(Vector3 start, Vector3 end, float t)
    {
        return new Vector3(
        FloatLerp(start.X, end.X, t),
        FloatLerp(start.Y, end.Y, t),
        FloatLerp(start.Z, end.Z, t)
        );
    }
}
