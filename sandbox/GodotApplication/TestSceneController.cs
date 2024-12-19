using Godot;

using GroveGames.Tween;
using GroveGames.Tween.Context;
using GroveGames.Tween.Core;
using GroveGames.Tween.Easing;
using GroveGames.Tween.Pooling;
using GroveGames.Tween.TweenExtensions;

using System;

public partial class TestSceneController : Node3D
{
	[Export] Node3D _cube;
	TweenerContext _context;

	public override void _Ready()
	{
		_context = new TweenerContext();
		_context.AddPoolStrategy<ITween<float>>(new FloatTweenPooledObjectStrategy());
		_context.AddPoolStrategy<ITween<Vector3>>(new Vector3PooledObjectStrategy());

		var sequence = _context.CreateSequnce();

		var moveRightTween = _cube.MoveXTo(_cube.GlobalPosition.X + 2f, 3f, _context);
		moveRightTween.SetOnComplete(() => GD.Print("First Tween is Completed"));
		moveRightTween.SetEase(EaseType.InBack);

		var rotateTween = _cube.RotateYTo(360, 4f, _context);

		var moveLeftTween = _cube.MoveXTo(_cube.GlobalPosition.X, 2f, _context);
		moveLeftTween.SetOnComplete(() => GD.Print("Second Tween is Completed"));

		var scaleTween = _cube.ScaleTo(Vector3.One, 1f, _context);
		scaleTween.SetEase(EaseType.OutBack);

		var shakeTween = _cube.ShakePosition(Vector3.One * 0.1f, 0.25f, _context, 10);

		sequence
		.Then(shakeTween)
		.Then(moveRightTween)
		.With(rotateTween)
		.Wait(1f)
		.With(_cube.ScaleTo(_cube.Scale * 0.25f, 1f, _context))
		.Callback(() => GD.Print("Callback 1 invoke"))
		.Wait(3f)
		.Callback(() => GD.Print("Callback 2 invoke"))
		.Then(moveLeftTween)
		.Then(scaleTween)
		.Then(_cube.RotateYTo(360, 2f, _context).SetLoops(LoopType.Yoyo, 3))
		.Then(_cube.RotateYTo(360, 2f, _context).SetLoops(LoopType.Restart, -1))
		.SetOnComplete(() => GD.Print("Sequence is Completed"));
	}

	public override void _Process(double delta)
	{
		_context.Update((float)delta);
	}
}
