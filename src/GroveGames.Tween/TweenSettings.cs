using GroveGames.Scheduling;

namespace GroveGames.Tween;

public readonly struct TweenSettings
{
    private readonly float _duration;
    private readonly Ease _ease;
    private readonly float _delay;
    private readonly int _loops;
    private readonly LoopType _loopType;
    private readonly float _loopInterval;
    private readonly TimeMode _timeMode;

    public float Duration => _duration;

    public Ease Ease => _ease;

    public float Delay => _delay;

    public int Loops => _loops;

    public LoopType LoopType => _loopType;

    public float LoopInterval => _loopInterval;

    public TimeMode TimeMode => _timeMode;

    public TweenSettings(float duration, Ease ease = Ease.Linear, float delay = 0f, int loops = 1, LoopType loopType = LoopType.Restart, float loopInterval = 0f, TimeMode timeMode = TimeMode.Scaled)
    {
        _duration = duration;
        _ease = ease;
        _delay = delay;
        _loops = loops;
        _loopType = loopType;
        _loopInterval = loopInterval;
        _timeMode = timeMode;
    }
}
