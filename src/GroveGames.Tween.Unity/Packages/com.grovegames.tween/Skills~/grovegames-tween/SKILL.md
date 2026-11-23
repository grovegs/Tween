---
name: grovegames-tween
description: Rules for animating in game code with GroveGames.Tween, covering allocation-free setters, time modes and sequences as timers. Use when adding or changing a tween, a sequence, a UI animation or a timed UI effect, or when code animates a value by hand in a frame update.
---

# Using GroveGames.Tween

Rules for game code that animates with this package. They hold in every game. The API is in the package's `README.md`.

- Animate through `ITweener`. Do not hand-roll a tween in a frame update.
- Bind with an inline `static` lambda and a state argument, so the tween allocates nothing: `.Bind(_alert, static (opacity, alert) => alert.SetOpacity(opacity))`. The compiler caches it.
- Keep a delegate in a field only when the same instance is needed again, for example to unsubscribe. A method group passed as a delegate allocates on every call.
- UI runs on unscaled time, because time can stop while a scene loads. A UI tween uses `.WithTimeMode(TimeMode.Unscaled)` and a UI sequence is created with `CreateSequence(TimeMode.Unscaled)`.
- A UI timer is an unscaled tween sequence, not a frame update.
- World-space effects that belong to the gameplay, such as damage labels, stay on scaled time.

## Unity

- Do not hand-roll a tween in a coroutine either.
