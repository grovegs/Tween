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

    private T _startValue;
    private T _endValue;

    private readonly Func<T, T, float, T> _lerpFunction;
    private readonly Func<T> _startValueFunc;
    private readonly Func<T> _endValueFunc;

    private Action? _onComplete;
    private Action<T>? _onUpdate;

    private EaseType _easeType;

    internal Tween(Func<T> start, Func<T> end, float duration, Func<T, T, float, T> lerpFunc, bool autoStart)
    {
        _startValueFunc = start;
        _endValueFunc = end;
        _duration = duration;
        _lerpFunction = lerpFunc;
        _isRunning = true;

        if (autoStart)
        {
            Play();
        }
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

    public ITween SetOnComplete(Action onComplete)
    {
        _onComplete += onComplete;
        return this;
    }

    public ITween SetOnUpdate(Action<T> onUpdate)
    {
        _onUpdate += onUpdate;
        return this;
    }

    public ITween SetEase(EaseType easeType)
    {
        _easeType = easeType;
        return this;
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
        _startValue = _startValueFunc();
        _endValue = _endValueFunc();
        _isPlaying = true;
    }
}