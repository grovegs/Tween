namespace GroveGames.Tween;

public interface ITweenAdapter<T>
{
    public T Evaluate(in T from, in T to, float progress);
}
