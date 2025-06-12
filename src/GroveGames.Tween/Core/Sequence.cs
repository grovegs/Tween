using GroveGames.Tween.Easing;

namespace GroveGames.Tween.Core;

internal class Sequence : ISequence
{
    private readonly List<ISequenceExecutable> _sequenceExecutables;
    private readonly HashSet<ISequenceExecutable> _completedExecutables;

    private float _elapsedTime;
    private float _duration;
    private float _lastAppendDuration;

    private bool _isPlaying;
    private bool _isRunning;
    private int _id;

    public bool IsRunning => _isRunning;
    public bool IsPlaying => _isPlaying;
    public float Duration => _duration;
    public int Id => _id;

    private Action? _onComplete;
    private Action? _onStop;

    internal Sequence()
    {
        _sequenceExecutables = [];
        _completedExecutables = [];
        _isPlaying = true;
        _isRunning = true;
        _id = -1;
    }

    internal void Construct(bool autoPlay = true)
    {
        _isRunning = true;
        _isPlaying = autoPlay;
    }

    public ISequence Then(ITween tween)
    {
        tween.Pause();
        _lastAppendDuration = tween.Duration;
        var element = new TweenSequenceExecutable(tween, _duration);
        element.OnComplete(() => _completedExecutables.Add(element));
        _duration += tween.Duration;
        _sequenceExecutables.Add(element);
        return this;
    }

    public ISequence With(ITween tween)
    {
        tween.Pause();
        var element = new TweenSequenceExecutable(tween, _duration - _lastAppendDuration);
        element.OnComplete(() => _completedExecutables.Add(element));
        _sequenceExecutables.Add(element);
        return this;
    }

    public ISequence Callback(Action callback)
    {
        var element = new ActionSequenceExecutable(callback, _duration);
        element.OnComplete(() => _completedExecutables.Add(element));
        _sequenceExecutables.Add(element);
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

    public ITween SetEase(EaseType easeType)
    {
        throw new ArgumentException("Sequences don't have easings");
    }

    public ITween SetLoops(LoopType loopType, int loopCount)
    {
        throw new ArgumentException("Sequences don't have loops");
    }

    public ITween SetOnComplete(Action onComplete)
    {
        _onComplete += onComplete;
        return this;
    }

    public ITween SetOnStop(Action onStop)
    {
        _onStop += onStop;
        return this;
    }

    public ITween SetId(int id)
    {
        _id = id;
        return this;
    }

    public void Stop()
    {
        _isRunning = false;

        foreach (var executable in _sequenceExecutables)
        {
            if (executable.IsRunning)
            {
                executable.Stop();
            }
        }

        _onStop?.Invoke();
    }

    public void Update(float deltaTime)
    {
        if (!_isPlaying || !_isRunning)
        {
            return;
        }

        _elapsedTime += deltaTime;

        for (var i = _sequenceExecutables.Count - 1; i >= 0; i--)
        {
            var currentElement = _sequenceExecutables[i];

            if (_completedExecutables.Contains(currentElement))
            {
                _sequenceExecutables.RemoveAt(i);
                _completedExecutables.Remove(currentElement);
                continue;
            }

            if (_elapsedTime >= currentElement.ExecutionTime && !currentElement.IsPlaying)
            {
                currentElement.Execute();
            }
        }

        if (_elapsedTime >= _duration && _sequenceExecutables.Count == 0)
        {
            _isRunning = false;
            _onComplete?.Invoke();
        }
    }

    public void Reset()
    {
        _sequenceExecutables.Clear();
        _completedExecutables.Clear();
        _id = -1;
        _onComplete = null;
        _onStop = null;
        _duration = 0f;
        _elapsedTime = 0f;
        _isPlaying = false;
        _isRunning = false;
    }
}