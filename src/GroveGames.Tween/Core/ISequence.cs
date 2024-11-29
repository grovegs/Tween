namespace GroveGames.Tween.Core;

public interface ISequence : ITween
{
    ISequence Append(in ITween tween);
    ISequence Join(in ITween tween);
    ISequence AppendInterval(float interval);
    ISequence AppendCallback(Action callback);
    ISequence Insert(float t, in ITween tween);
    ISequence InsertCallback(float t, Action callback);
    void Reset();
}
