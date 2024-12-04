namespace GroveGames.Tween.Core;

public interface ISequence : ITween
{
    ISequence Append(ITween tween);
    ISequence Join(ITween tween);
    ISequence AppendInterval(float interval);
    ISequence AppendCallback(Action callback);
}
