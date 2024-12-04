# GroveGames.Tween

[![Build Status](https://github.com/grovegs/Tween/actions/workflows/release.yml/badge.svg)](https://github.com/grovegs/Tween/actions/workflows/release.yml)
[![Tests](https://github.com/grovegs/Tween/actions/workflows/tests.yml/badge.svg)](https://github.com/grovegs/Tween/actions/workflows/tests.yml)
[![Latest Release](https://img.shields.io/github/v/release/grovegs/Tween)](https://github.com/grovegs/Tween/releases/latest)
[![NuGet](https://img.shields.io/nuget/v/GroveGames.Tween)](https://www.nuget.org/packages/GroveGames.Tween)

GroveGames.Tween is a lightweight and extensible tweening library for Godot, designed to animate values smoothly over time. It provides an easy-to-use API for creating tweens and sequences, with built-in easing functions for smooth transitions.

## Features

- **Flexible Tweening**: Tween any type of value, including `float`, `Vector3`, and more.
- **Ease Functions**: Use predefined easing functions to customize the motion.
- **Sequences**: Chain tweens and callbacks for complex animations.
- **Extensions**: Built-in support for animating properties of `Godot Nodes`, such as position, rotation, and scale.
- **Customizable Behavior**: Easily add callbacks for updates and completions.

---

## Installation

### Using NuGet

To add GroveGames.Tween.Godot to your project, install the NuGet package:

```bash
dotnet add package GroveGames.Tween.Godot
```

Alternatively, use the Package Manager Console in Visual Studio:

```bash
Install-Package GroveGames.Tween.Godot
```

Once installed, ensure that your project references the `GroveGames.Tween` namespace and its sub-namespaces.

---

## Usage

### Basic Tween Example

```csharp
using Godot;
using GroveGames.Tween.Core;

public class Example : Node3D
{
    private TweenerContext _tweenContext;

    public override void _Ready()
    {
        _tweenContext = new TweenerContext();

        // Move this Node3D to a new position over 2 seconds
        this.MoveTo(new Vector3(5, 5, 0), 2f, _tweenContext)
            .SetOnComplete(() => GD.Print("Move completed!"));
    }

    public override void _Process(float delta)
    {
        // Update tweens
        _tweenContext.Update(delta);
    }
}
```

### Using Sequences

```csharp
using Godot;
using GroveGames.Tween.Core;

public class SequenceExample : Node3D
{
    private TweenerContext _tweenContext;

    public override void _Ready()
    {
        _tweenContext = new TweenerContext();

        var sequence = _tweenContext.CreateSequence();
        sequence
            .Then(this.MoveTo(new Vector3(5, 5, 0), 2f, _tweenContext, false))
            .Wait(1f)
            .Callback(() => GD.Print("Reached target!"))
            .Play();
    }

    public override void _Process(float delta)
    {
        _tweenContext.Update(delta);
    }
}
```

### Extension Methods

The library includes handy extension methods for `Node3D, Node2D, Transform3D, Transform2D, etc.` objects. Examples include:

- `MoveTo(Vector3 target, float duration, TweenerContext context)`
- `RotateTo(Vector3 targetDegrees, float duration, TweenerContext context)`
- `ScaleTo(Vector3 target, float duration, TweenerContext context)`

Each method has variations to animate specific axes (e.g., `MoveXTo`, `RotateYTo`).

---

## API Reference

### Tween Interfaces

#### `ITween`

- `void SetEase(EaseType easeType)`
- `void SetOnComplete(Action onComplete)`
- `void Stop(bool complete)`
- `void Update(float deltaTime)`
- `void Pause()`
- `void Play()`
- `bool IsRunning`
- `bool IsPlaying`
- `float Duration`

#### `ITween<T>` (Generic)

- `void SetOnUpdate(Action<T> onUpdate)`

#### `ISequence`

- `ISequence Then(ITween tween)`
- `ISequence With(ITween tween)`
- `ISequence Wait(float interval)`
- `ISequence Callback(Action callback)`
- `void Reset()`

---

## Customization

### Easing Functions

Customize your tweening with easing functions defined in the `EaseType` enum. Example:

```csharp
tween.SetEase(EaseType.QuadInOut);
```

### Custom Lerp Functions

Provide your own interpolation function for advanced use cases:

```csharp
var tween = context.CreateTween(
    () => currentValue, 
    () => targetValue, 
    duration, 
    (start, end, t) => start + (end - start) * t
);
```

---

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests to improve this library.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
