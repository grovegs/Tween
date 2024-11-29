using System;

using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

internal class Sequence : ISequence
{
    private readonly struct SequenceTweenElement
    {
        public readonly float ExecutionTime;
        public readonly ITween Tween;

        public SequenceTweenElement(float time, in ITween tween)
        {
            ExecutionTime = time;
            Tween = tween;
        }
    }

    private readonly struct SequenceCallbackElement
    {
        public readonly float ExecutionTime;
        public readonly Action Callback;

        public SequenceCallbackElement(float time, Action callback)
        {
            ExecutionTime = time;
            Callback = callback;
        }
    }

    private readonly List<SequenceTweenElement> _sequenceTweenElements;
    private readonly List<SequenceCallbackElement> _sequenceCallbackElements;

    private float _currentInterval;
    private float _elapsedTime;

    private bool _isPlaying;
    private bool _isRunning;

    public bool IsRunning => _isRunning;
    public bool IsPlaying => _isPlaying;
    public float Duration => _currentInterval;

    private EaseType _easeType;

    private Action? _onComplete;

    public Sequence()
    {
        _sequenceTweenElements = [];
        _sequenceCallbackElements = [];
        _isRunning = true;
        _isPlaying = true;
        _easeType = EaseType.Linear;
    }

    public void Append(in ITween tween)
    {
        var element = new SequenceTweenElement(_currentInterval, in tween);
        _sequenceTweenElements.Add(element);

        _currentInterval += tween.Duration;
    }

    public void Join(in ITween tween)
    {
        var element = new SequenceTweenElement(_currentInterval, in tween);
        _sequenceTweenElements.Add(element);
    }

    public void AppendCallback(Action callback)
    {
        var element = new SequenceCallbackElement(_currentInterval, callback);
        _sequenceCallbackElements.Add(element);
    }

    public void AppendInterval(float interval)
    {
        _currentInterval += interval;
    }

    public void Insert(float t, in ITween tween)
    {
        var element = new SequenceTweenElement(t, in tween);
        _sequenceTweenElements.Add(element);
    }

    public void InsertCallback(float t, Action callback)
    {
        var element = new SequenceCallbackElement(t, callback);
        _sequenceCallbackElements.Add(element);
    }

    public void Pause()
    {
        _isPlaying = false;
    }

    public void Play()
    {
        _isPlaying = true;
    }

    public ITween SetEase(EaseType easeType)
    {
        _easeType = easeType;
        return this;
    }

    public ITween SetOnComplete(Action onComplete)
    {
        _onComplete += onComplete;
        return this;
    }

    public void Stop(bool complete)
    {
        _isRunning = false;
    }

    public void Update(float deltaTime)
    {
        if (!_isPlaying || !_isRunning)
        {
            return;
        }

        _elapsedTime += deltaTime;
        var progress = _elapsedTime / Duration;
        var easedTime = deltaTime * EaseCalculations.Evaluate(_easeType, progress);
        _elapsedTime = easedTime * Duration;

        for (var i = 0; i < _sequenceTweenElements.Count; i++)
        {
            var currentElement = _sequenceTweenElements[i];
            if (_elapsedTime >= currentElement.ExecutionTime && currentElement.Tween.IsRunning)
            {
                currentElement.Tween.Update(deltaTime);
            }

            else if (_elapsedTime >= currentElement.ExecutionTime && !currentElement.Tween.IsRunning)
            {
                _sequenceTweenElements.RemoveAt(i);
            }
        }

        for (var i = 0; i < _sequenceCallbackElements.Count; i++)
        {
            var currentCallback = _sequenceCallbackElements[i];
            if (_elapsedTime >= currentCallback.ExecutionTime)
            {
                currentCallback.Callback?.Invoke();
                _sequenceCallbackElements.RemoveAt(i);
            }
        }

        if (_sequenceTweenElements.Count == 0)
        {
            _onComplete?.Invoke();
            _isRunning = false;
        }
    }

    public void Reset()
    {
        _onComplete = null;
        _sequenceTweenElements.Clear();
        _sequenceCallbackElements.Clear();
        _currentInterval = 0f;
        _elapsedTime = 0f;
        _isPlaying = true;
        _easeType = EaseType.Linear;
    }
}
