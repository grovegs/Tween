# CLAUDE.md

## Package

- The core is the `GroveGames.Tween` NuGet package at `src/GroveGames.Tween` (`net10.0`, `net9.0`, `net8.0`, `netstandard2.1`). It depends on `GroveGames.Scheduling` for `TimeMode` and `ScheduleToken`, and on `System.Runtime.CompilerServices.Unsafe` for netstandard2.1. Tests are in `tests/GroveGames.Tween.Tests` (xUnit v3), benchmarks in `sandbox/DotnetBenchmark`.
- `Tweener` keeps one `TweenStorage<T, TAdapter>` per value type and adapter, created on first use and indexed by `TweenStorageId<T, TAdapter>`. Floats are built in. Other types go through the generic `Create<T, TAdapter>`, `CreatePunch<T, TAdapter>` and `CreateShake<T, TAdapter>`. Storages update in creation order, then sequence callbacks.
- Exceptions from setters and callbacks go to the handler passed to the `Tweener` constructor. The tween that threw is removed and the others keep running.
- The Unity package at `src/GroveGames.Tween.Unity/Packages/com.grovegames.tween` references `GroveGames.Tween.dll` and `GroveGames.Scheduling.dll`, installed with NuGetForUnity. `TweenerExtensions` adds `Vector2`, `Vector3` and `Color` overloads on `ITweener`. They call the generic methods on the sealed `Tweener` directly when they can, because generic interface calls are slow in IL2CPP. There is no Godot addon.
- The GroveGames.DependencyInjection integration in `Integrations/GroveGames.DependencyInjection` compiles only when that package is installed. It adds `AddTweener()`, which logs exceptions with `Debug.LogException`, and `TweenerUpdater` feeds Unity's deltas to `Update` before each frame.
- Performance matters. Creating, updating and canceling tweens must not allocate after warm-up, and `TweenerAllocationTests` checks that in both xUnit and Unity.

## Code Rules

- No comments.
- Unity code uses block namespaces and C# 10.
- Every file and folder in the package needs a `.meta` with a 32-character lowercase hex GUID. Existing asmdef GUIDs must not change, because projects reference them.
- Tests are named `MethodName_Condition_Expected`, with arrange, act and assert separated by blank lines. Unity tests are EditMode tests in `Tests/Editor`.
