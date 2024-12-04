namespace GroveGames.Tween.Core;

internal class ActionSequenceExecutable : ISequenceExecutable
{
    public float ExecutionTime => _executionTime;

    private float _executionTime;
    private Action _action;

    public ActionSequenceExecutable(Action action, float executionTime)
    {
        _executionTime = executionTime;
        _action = action;
    }

    public void Execute()
    {
        _action?.Invoke();
    }

    public void Reset()
    {
        _action = null;
        _executionTime = 0f;
    }
}

