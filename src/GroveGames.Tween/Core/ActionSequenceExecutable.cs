namespace GroveGames.Tween.Core;

internal class ActionSequenceExecutable : ISequenceExecutable
{
    public float ExecutionTime => _executionTime;

    public bool IsRunning => false;

    public bool IsPlaying => false;

    private readonly float _executionTime;
    private readonly Action _action;

    public ActionSequenceExecutable(Action action, float executionTime)
    {
        _executionTime = executionTime;
        _action = action;
    }

    public void Execute()
    {
        _action?.Invoke();
    }

    public void Stop()
    {

    }
}

