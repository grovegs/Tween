using System.Collections.Generic;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

public interface ITweener
{
    public TweenBuilder<float> Create(float from, float to, float duration);

    public TweenBuilder<float> Create(float from, float to, in TweenSettings settings);

    public TweenBuilder<float> CreateShake(float from, IReadOnlyList<float> points, float duration);

    public TweenBuilder<T> Create<T, TAdapter>(T from, T to, float duration) where TAdapter : struct, ITweenAdapter<T>;

    public TweenBuilder<T> Create<T, TAdapter>(T from, T to, in TweenSettings settings) where TAdapter : struct, ITweenAdapter<T>;

    public TweenBuilder<T> CreatePunch<T, TAdapter>(T from, T to, float duration, int frequency = 10, float dampingRatio = 1f) where TAdapter : struct, ITweenAdapter<T>;

    public TweenBuilder<T> CreateShake<T, TAdapter>(T from, IReadOnlyList<T> points, float duration) where TAdapter : struct, ITweenAdapter<T>;

    public TweenSequenceBuilder CreateSequence(TimeMode timeMode = TimeMode.Scaled, ScheduleToken? token = null);
}
