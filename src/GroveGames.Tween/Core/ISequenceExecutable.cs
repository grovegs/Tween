namespace GroveGames.Tween.Core;

internal interface ISequenceExecutable
{
    void Execute();
    float ExecutionTime { get; }
    void Reset();
}

