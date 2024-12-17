using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

public interface ITween
{
    ITween SetEase(EaseType easeType);
    ITween SetOnComplete(Action onComplete);
    ITween SetId(int id);
    ITween SetLoops(LoopType loopType, int loopCount);
    void Stop(bool complete);
    void Update(float deltaTime);
    void Pause();
    void Play();
    void Reset();
    bool IsRunning { get; }
    bool IsPlaying { get; }
    float Duration { get; }
    int Id { get; }
}

public interface ITween<T> : ITween
{
    ITween SetOnUpdate(Action<T> onUpdate);
}
