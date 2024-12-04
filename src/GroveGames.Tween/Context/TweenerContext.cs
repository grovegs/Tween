using GroveGames.Tween.Core;

namespace GroveGames.Tween.Context;

public class TweenerContext
{
    private readonly List<ITween> _tweens;
    private readonly HashSet<ITween> _stoppedTweens;

    public TweenerContext()
    {
        _tweens = [];
        _stoppedTweens = [];
    }

    public ITween<T> CreateTween<T>(Func<T> start, Func<T> end, float duration, Func<T, T, float, T> lerpFunc, bool autoPlay)
    {
        ITween<T> tween = new Tween<T>(start, end, duration, lerpFunc: lerpFunc, autoPlay);
        tween.SetOnComplete(() => _stoppedTweens.Add(tween));
        _tweens.Add(tween);

        return tween;
    }

    public void Stop(ITween tween)
    {
        _stoppedTweens.Add(tween);
    }

    public void Stop(int id)
    {
        foreach (var tween in _tweens)
        {
            if (tween.Id == id)
            {
                Stop(tween);
                break;
            }
        }
    }

    public ISequence CreateSequnce()
    {
        ISequence sequence = new Sequence();
        sequence.SetOnComplete(() => _stoppedTweens.Add(sequence));
        _tweens.Add(sequence);

        return sequence;
    }

    public void Update(float deltaTime)
    {
        for (var i = _tweens.Count - 1; i >= 0; i--)
        {
            var tween = _tweens[i];

            if (_stoppedTweens.Contains(tween))
            {
                _stoppedTweens.Remove(tween);
                _tweens.RemoveAt(i);
                tween.Stop(false);
                continue;
            }

            if (!tween.IsRunning)
            {
                _stoppedTweens.Add(tween);
                continue;
            }

            tween.Update(deltaTime);
        }
    }
}
