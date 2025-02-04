namespace GroveGames.Tween.Core;

internal class TweenSequenceExecetable : ISequenceExecutable
{
    public float ExecutionTime => _executionTime;

    public bool IsRunning => _tween.IsRunning;

    public bool IsPlaying => _tween.IsPlaying;

    private readonly float _executionTime;
    private readonly ITween _tween;

    public TweenSequenceExecetable(ITween tween, float executionTime)
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
}

