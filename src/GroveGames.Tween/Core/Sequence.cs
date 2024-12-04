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
    private float _duration;

    private bool _isPlaying;
    private bool _isRunning;

    public bool IsRunning => _isRunning;
    public bool IsPlaying => _isPlaying;
    public float Duration => _duration;

    public int Id => _id;
    private int _id;

    private Action _onComplete;

    public Sequence()
    {
        _sequenceTweenElements = [];
        _sequenceCallbackElements = [];
        _isRunning = true;
        _isPlaying = true;
    }

    public ISequence Append(ITween tween)
    {
        tween.Pause();
        if (_sequenceTweenElements.Count > 0)
        {
            _currentInterval += _sequenceTweenElements[^1].Tween.Duration;
        }

        _duration += tween.Duration;
        var element = new SequenceTweenElement(_currentInterval, in tween);
        _sequenceTweenElements.Add(element);
        return this;
    }

    public ISequence Join(ITween tween)
    {
        tween.Pause();
        var element = new SequenceTweenElement(_currentInterval, in tween);
        _sequenceTweenElements.Add(element);
        return this;
    }

    public ISequence AppendCallback(Action callback)
    {
        var element = new SequenceCallbackElement(_currentInterval, callback);
        _sequenceCallbackElements.Add(element);
        return this;
    }

    public ISequence AppendInterval(float interval)
    {
        _currentInterval += interval;
        _duration += interval;
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
        _currentInterval = 0f;
        _elapsedTime = 0f;
        _isPlaying = true;
    }

    public void SetId(int id)
    {
        _id = id;
    }
}
