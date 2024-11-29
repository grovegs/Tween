using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

public interface ITween
{
    ITween SetEase(EaseType easeType);
    ITween SetOnComplete(Action onComplete);
    void Stop(bool complete);
    void Update(float deltaTime);
    void Pause();
    void Play();
    bool IsRunning { get; }
    bool IsPlaying { get; }
    float Duration { get; }
}

public interface ITween<T> : ITween
{
    ITween SetOnUpdate(Action<T> onUpdate);
}
