# GroveGames.Tween

Zero-allocation tweens and sequences for .NET and Unity.

[![NuGet](https://img.shields.io/nuget/v/GroveGames.Tween)](https://www.nuget.org/packages/GroveGames.Tween)
[![Build Status](https://github.com/grovegs/Tween/actions/workflows/release.yml/badge.svg)](https://github.com/grovegs/Tween/actions/workflows/release.yml)
[![Tests](https://github.com/grovegs/Tween/actions/workflows/tests.yml/badge.svg)](https://github.com/grovegs/Tween/actions/workflows/tests.yml)
[![Latest Release](https://img.shields.io/github/v/release/grovegs/Tween)](https://github.com/grovegs/Tween/releases/latest)

---

## Features

- **Core**: `ITweener` and `Tweener` in the `GroveGames.Tween` NuGet package tween floats out of the box and any other type through an `ITweenAdapter<T>` struct. Tweens of each type live in their own storage, so creating, updating and canceling them allocates nothing after warm-up.
- **Builder**: `Create(from, to, duration)` returns a `TweenBuilder<T>`. `WithEase`, `WithDelay`, `WithLoops`, `WithTimeMode`, `WithToken` and `WithCompletionCallback` return a modified copy, and `Bind(state, setter)` starts the tween with a static setter that captures nothing.
- **Eases and loops**: 31 eases, `Restart` and `Yoyo` loops with an optional interval between them, punch tweens that spring back to the start, and shake tweens that move through caller-provided points and back.
- **Sequences**: `CreateSequence()` chains tweens with `Append`, `Join`, `AppendInterval` and `AppendCallback`, and cancels them together.
- **Clocks and cancellation**: Scaled, unscaled and realtime clocks and `ScheduleToken` come from [GroveGames.Scheduling](https://github.com/grovegs/Scheduling). Handles report `IsActive` and `Cancel()` a single tween or sequence.
- **Unity**: The Unity package adds `Vector2`, `Vector3` and `Color` tweens, `CreatePunch` for `Vector3` and `CreateShake` for `Vector2` and `Vector3`.
- **Dependency injection**: `AddTweener()` registers `ITweener` with [GroveGames.DependencyInjection](https://github.com/grovegs/DependencyInjection), logs callback exceptions with `Debug.LogException` and updates the tweener from Unity's time every frame.

## Installation

### .NET

```bash
dotnet add package GroveGames.Tween
```

### Unity

Install the core through [NuGetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) (`GroveGames.Tween`, which brings `GroveGames.Scheduling`), then add both packages to `Packages/manifest.json`:

```json
{
    "dependencies": {
        "com.grovegames.scheduling": "https://github.com/grovegs/Scheduling.git?path=src/GroveGames.Scheduling.Unity/Packages/com.grovegames.scheduling",
        "com.grovegames.tween": "https://github.com/grovegs/Tween.git?path=src/GroveGames.Tween.Unity/Packages/com.grovegames.tween"
    }
}
```

The core uses C# 10, so add a `csc.rsp` to `Assets/`:

```text
-langversion:10
-nullable:enable
```

## Usage

Create a tweener with a handler for exceptions thrown by setters and callbacks, and update it once per frame:

```csharp
var tweener = new Tweener(Debug.LogException);

tweener.Update(deltaTime, unscaledDeltaTime, realtimeDeltaTime);
```

Bind a tween to a state object with a static setter so it allocates nothing:

```csharp
var handle = tweener.Create(0f, 1f, 0.3f)
    .WithEase(Ease.OutQuad)
    .Bind(canvasGroup, static (alpha, group) => group.alpha = alpha);

handle.Cancel();
```

Chain tweens in a sequence:

```csharp
tweener.CreateSequence()
    .Append(tweener.Create(Vector3.one, Vector3.one * 1.2f, 0.1f), transform, static (scale, target) => target.localScale = scale)
    .Append(tweener.Create(Vector3.one * 1.2f, Vector3.one, 0.1f), transform, static (scale, target) => target.localScale = scale)
    .AppendCallback(onPressed);
```

Tween another type with an adapter:

```csharp
readonly struct DoubleAdapter : ITweenAdapter<double>
{
    public double Evaluate(in double from, in double to, float progress)
    {
        return from + (to - from) * progress;
    }
}

tweener.Create<double, DoubleAdapter>(0d, 100d, 1f).Bind(counter, static (value, target) => target.Value = value);
```

With GroveGames.DependencyInjection installed, register it in an installer and inject `ITweener`:

```csharp
builder.AddTweener();
```

## Testing

```bash
dotnet test
```

The Unity EditMode tests run from `sandbox/UnityApplication` with the Unity Test Runner.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
