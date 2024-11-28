using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

internal interface ITween
{
    void SetEase(EaseType easeType);
    void SetOnComplete(Action onComplete);
    void Stop(bool complete);
    void Update(float deltaTime);
    void Pause();
    void Play();
    bool IsRunning { get; }
    bool IsPlaying { get; }
}

internal interface ITween<T> : ITween
{
    void SetOnUpdate(Action<T> onUpdate);
}
