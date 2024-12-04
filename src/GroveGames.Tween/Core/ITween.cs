using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

public interface ITween
{
    void SetEase(EaseType easeType);
    void SetOnComplete(Action onComplete);
    void SetId(int id);
    void Stop(bool complete);
    void Update(float deltaTime);
    void Pause();
    void Play();
    bool IsRunning { get; }
    bool IsPlaying { get; }
    float Duration { get; }
    int Id { get; }
}

public interface ITween<T> : ITween
{
    void SetOnUpdate(Action<T> onUpdate);
}
