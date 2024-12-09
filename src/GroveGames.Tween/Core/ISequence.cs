namespace GroveGames.Tween.Core;

public interface ISequence : ITween
{
    ISequence Then(ITween tween);
    ISequence With(ITween tween);
    ISequence Wait(float duration);
    ISequence Callback(Action callback);
}
