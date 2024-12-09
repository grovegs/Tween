using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

internal class Tween<T> : ITween<T>
{
    public bool IsRunning => _isRunning;
    public bool IsPlaying => _isPlaying;

    public float Duration => _duration;

    public int Id => _id;

    private readonly float _duration;
    private float _elapsedTime;
    private int _id;

    private bool _isRunning;
    private bool _isPlaying;

    private T _startValue;
    private T _endValue;

    private Func<T, T, float, T> _lerpFunction;
    private Func<T> _startValueFunc;
    private Func<T> _endValueFunc;

    private Action _onComplete;
    private Action<T> _onUpdate;

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

    public ITween SetId(int id)
    {
        _id = id;
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

    public void Reset()
    {
        _id = -1;
        _elapsedTime = 0f;
        _isPlaying = true;
        _isRunning = true;
        _onComplete = null;
        _onUpdate = null;
        _endValueFunc = null;
        _lerpFunction = null;
        _startValueFunc = null;
        _easeType = EaseType.Linear;
    }
}