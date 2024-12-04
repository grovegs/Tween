namespace GroveGames.Tween.Core;

internal class TweenSequenceExecetable : ISequenceExecutable
{
    public float ExecutionTime => _executionTime;

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
}

