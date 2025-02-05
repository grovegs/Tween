
namespace GroveGames.Tween.Core;

internal class TweenSequenceExecutable : ISequenceExecutable
{
    public float ExecutionTime => _executionTime;

    public bool IsRunning => _tween.IsRunning;

    public bool IsPlaying => _tween.IsPlaying;

    private readonly float _executionTime;
    private readonly ITween _tween;

    public TweenSequenceExecutable(ITween tween, float executionTime)
    {
        _executionTime = executionTime;
        _tween = tween;
    }

    public void Execute()
    {
        _tween.Play();
    }

    public void Stop()
    {
        _tween.Stop();
    }

    public void OnComplete(Action onComplete)
    {
        _tween.SetOnComplete(onComplete);
    }
}

