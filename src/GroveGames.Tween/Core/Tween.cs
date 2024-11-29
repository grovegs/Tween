using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

internal struct Tween<T> : ITween<T>
{
    public readonly bool IsRunning => _isRunning;
    public readonly bool IsPlaying => _isPlaying;

    public readonly float Duration => _duration;

    private readonly float _duration;
    private float _elapsedTime;

    private bool _isRunning;
    private bool _isPlaying;

    private readonly T _startValue;
    private readonly T _endValue;

    private readonly Func<T, T, float, T> _lerpFunction;
    private Action? _onComplete;
    private Action<T>? _onUpdate;

    private EaseType _easeType;

    internal Tween(T start, T end, float duration, Func<T, T, float, T> lerpFunc, bool autoStart)
    {
        _startValue = start;
        _endValue = end;
        _duration = duration;
        _lerpFunction = lerpFunc;
        _isRunning = true;
        _isPlaying = autoStart;
    }

    public void Update(float deltaTime)
    {
        if (!_isRunning || !_isPlaying)
        {
            return;
        }

        _elapsedTime += deltaTime;

        if (_elapsedTime >= _duration)
        {
            _elapsedTime = _duration;
            _isRunning = false;
        }

        var t = Math.Clamp(_elapsedTime / _duration, 0f, 1f);
        var easeT = EaseCalculations.Evaluate(_easeType, t);
        var value = _lerpFunction(_startValue, _endValue, easeT);

        _onUpdate?.Invoke(value);

        if (!_isRunning)
        {
            _onComplete?.Invoke();
        }
    }

    public void SetOnComplete(Action onComplete)
    {
        _onComplete += onComplete;
    }

    public void SetOnUpdate(Action<T> onUpdate)
    {
        _onUpdate += onUpdate;
    }

    public void SetEase(EaseType easeType)
    {
        _easeType = easeType;
    }

    public void Pause()
    {
        _isPlaying = false;
    }

    public void Stop(bool complete)
    {
        _isRunning = false;

        if (complete)
        {
            _onUpdate?.Invoke(_endValue);
            _onComplete?.Invoke();
        }
    }

    public void Play()
    {
        _isPlaying = true;
    }
}