namespace GroveGames.Tween.Core;

internal class ActionSequenceExecutable : ISequenceExecutable
{
    public float ExecutionTime => _executionTime;

    public bool IsRunning => false;

    public bool IsPlaying => false;

    private readonly float _executionTime;
    private readonly Action _action;
    private Action _onComplete;

    public ActionSequenceExecutable(Action action, float executionTime)
    {
        _executionTime = executionTime;
        _action = action;
    }

    public void Execute()
    {
        _action?.Invoke();
        _onComplete?.Invoke();
    }

    public void Stop()
    {

    }

    public void OnComplete(Action onComplete)
    {
        _onComplete = onComplete;
    }
}

