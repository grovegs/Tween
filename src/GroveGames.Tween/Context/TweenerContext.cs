using System;

using GroveGames.Tween.Core;

namespace GroveGames.Tween.Context;

public class TweenerContext
{
    private readonly List<ITween> _tweens;
    private readonly HashSet<ITween> _killedTweens;

    public TweenerContext()
    {
        _tweens = [];
        _killedTweens = [];
    }

    public ITween Create<T>(T start, T end, float duration, Func<T, T, float, T> lerpFunc, bool autoPlay)
    {
        ITween tween = new Tween<T>(start, end, duration, lerpFunc: lerpFunc, autoPlay);
        tween.SetOnComplete(() => _killedTweens.Add(tween));
        _tweens.Add(tween);

        return tween;
    }

    public void Update(float deltaTime)
    {
        for (var i = _tweens.Count - 1; i >= 0; i--)
        {
            var tween = _tweens[i];

            if (_killedTweens.Contains(tween))
            {
                _killedTweens.Remove(tween);
                _tweens.RemoveAt(i);
                tween.Stop(false);
                continue;
            }

            if (!tween.IsRunning)
            {
                continue;
            }

            tween.Update(deltaTime);
        }
    }
}
