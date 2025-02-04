namespace GroveGames.Tween.Core;

internal interface ISequenceExecutable
{
    void Execute();
    void Stop();
    float ExecutionTime { get; }
    bool IsRunning { get; }
    bool IsPlaying { get; }
}

