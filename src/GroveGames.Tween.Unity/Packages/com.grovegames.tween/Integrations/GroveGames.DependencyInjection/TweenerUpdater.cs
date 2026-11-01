using GroveGames.DependencyInjection;
using UnityEngine;

namespace GroveGames.Tween.Unity
{
    internal sealed class TweenerUpdater : IPreFrameUpdatable
    {
        private readonly Tweener _tweener;
        private double _realtime;

        public TweenerUpdater(Tweener tweener)
        {
            _tweener = tweener;
            _realtime = Time.realtimeSinceStartupAsDouble;
        }

        public void PreFrameUpdate(float deltaTime)
        {
            var realtime = Time.realtimeSinceStartupAsDouble;
            var realtimeDeltaTime = (float)(realtime - _realtime);
            _realtime = realtime;
            _tweener.Update(deltaTime, Time.unscaledDeltaTime, realtimeDeltaTime);
        }
    }
}
