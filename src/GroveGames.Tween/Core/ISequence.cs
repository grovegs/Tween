namespace GroveGames.Tween.Core;

public interface ISequence : ITween
{
    void Append(in ITween tween);
    void Join(in ITween tween);
    void AppendInterval(float interval);
    void AppendCallback(Action callback);
    void Insert(float t, in ITween tween);
    void InsertCallback(float t, Action callback);
    void Reset();
}
