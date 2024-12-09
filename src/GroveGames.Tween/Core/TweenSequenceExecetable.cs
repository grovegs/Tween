namespace GroveGames.Tween.Core;

internal class TweenSequenceExecetable : ISequenceExecutable
{
    public float ExecutionTime => _executionTime;

    private float _executionTime;
    private ITween _tween;

    public TweenSequenceExecetable(ITween tween, float executionTime)
    {
        _executionTime = executionTime;
        _tween = tween;
    }

    public void Execute()
    {
        _tween?.Play();
    }

    public void Reset()
    {
        _tween = null;
        _executionTime = 0f;
    }
}

