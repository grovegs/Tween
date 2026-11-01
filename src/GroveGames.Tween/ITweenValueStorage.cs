using System;
using System.Collections.Generic;
using GroveGames.Scheduling;

namespace GroveGames.Tween;

internal interface ITweenValueStorage<T> : ITweenStorage
{
    public int Id { get; }

    public int Add(T from, T to, in TweenTiming timing, TimeMode timeMode, object state, Action<T, object> setter, Action? completed, IReadOnlyList<T>? points, ScheduleToken? token, int sequenceSlot, int sequenceVersion, out int version);
}
