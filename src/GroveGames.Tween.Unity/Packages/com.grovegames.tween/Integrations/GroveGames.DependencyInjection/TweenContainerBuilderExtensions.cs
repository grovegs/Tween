using GroveGames.DependencyInjection;
using UnityEngine;

namespace GroveGames.Tween.Unity
{
    public static class TweenContainerBuilderExtensions
    {
        public static IContainerBuilder AddTweener(this IContainerBuilder builder)
        {
            builder.AddSingleton<Tweener>(_ => new Tweener(Debug.LogException));
            builder.AddSingleton<ITweener>(resolver => resolver.Resolve<Tweener>());
            return builder.AddSingleton<TweenerUpdater>();
        }
    }
}
