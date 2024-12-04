namespace GroveGames.Tween.Core;

internal class ActionSequenceExecutable : ISequenceExecutable
{
    public float ExecutionTime => _executionTime;

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
}

