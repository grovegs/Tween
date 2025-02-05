namespace GroveGames.Tween.Core;

internal interface ISequenceExecutable
{
    void Execute();
    void Stop();
    void OnComplete(Action onComplete);
    float ExecutionTime { get; }
    bool IsRunning { get; }
    bool IsPlaying { get; }
}

