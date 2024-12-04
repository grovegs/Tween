using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

internal class Sequence : ISequence
{
    private readonly struct SequenceTweenElement
    {
        public readonly float ExecutionTime;
        public readonly ITween Tween;

        public SequenceTweenElement(float time, ITween tween)
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

    private float _elapsedTime;
    private float _duration;
    private float _lastAppendDuration;

    private bool _isPlaying;
    private bool _isRunning;

    public bool IsRunning => _isRunning;
    public bool IsPlaying => _isPlaying;
    public float Duration => _duration;

    public int Id => _id;
    private int _id;

    private Action _onComplete;

    internal Sequence()
    {
        _sequenceTweenElements = [];
        _sequenceCallbackElements = [];
        _isRunning = true;
        _isPlaying = true;
    }

    public ISequence Then(ITween tween)
    {
        tween.Pause();

        _lastAppendDuration = tween.Duration;
        var element = new SequenceTweenElement(_duration, tween);
        _duration += tween.Duration;
        _sequenceTweenElements.Add(element);
        return this;
    }

    public ISequence With(ITween tween)
    {
        tween.Pause();
        var element = new SequenceTweenElement(_duration - _lastAppendDuration, tween);
        _sequenceTweenElements.Add(element);
        return this;
    }

    public ISequence Callback(Action callback)
    {
        var element = new SequenceCallbackElement(_duration, callback);
        _sequenceCallbackElements.Add(element);
        return this;
    }

    public ISequence Wait(float duration)
    {
        _lastAppendDuration = duration;
        _duration += duration;
        return this;
    }

    public void Pause()
    {
        _isPlaying = false;
    }

    public void Play()
    {
        _isPlaying = true;
    }

    public void SetEase(EaseType easeType)
    {
        // sequences don't have easing
    }

    public void SetOnComplete(Action onComplete)
    {
        _onComplete += onComplete;
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
        if (_elapsedTime >= _duration)
        {
            _isRunning = false;
        }

        for (var i = 0; i < _sequenceTweenElements.Count; i++)
        {
            var currentElement = _sequenceTweenElements[i];
            if (_elapsedTime >= currentElement.ExecutionTime && currentElement.Tween.IsRunning && !currentElement.Tween.IsPlaying)
            {
                currentElement.Tween.Play();
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

        if (!_isRunning)
        {
            _onComplete?.Invoke();
        }
    }

    public void Reset()
    {
        _id = -1;
        _onComplete = null;
        _sequenceTweenElements.Clear();
        _sequenceCallbackElements.Clear();
        _duration = 0f;
        _elapsedTime = 0f;
        _isPlaying = true;
        _isRunning = true;
    }

    public void SetId(int id)
    {
        _id = id;
    }
}
