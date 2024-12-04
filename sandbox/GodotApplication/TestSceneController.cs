using Godot;

using GroveGames.Tween.Context;
using GroveGames.Tween.Easing;
using GroveGames.Tween.TweenExtensions;

using System;

public partial class TestSceneController : Node3D
{
	[Export] Node3D _cube;
	TweenerContext _context;

	public override void _Ready()
	{
		_context = new TweenerContext();
		var sequence = _context.CreateSequnce();

		var moveUpTween = _cube.MoveLocalYTo(_cube.GlobalPosition.Y + 0.5f, 2f, _context);

		var moveRightTween = _cube.MoveXTo(_cube.GlobalPosition.X + 2f, 3f, _context);
		moveRightTween.SetOnComplete(() => GD.Print("First Tween is Completed"));
		moveRightTween.SetEase(EaseType.InBack);
		sequence.Append(moveRightTween);

		var rotateTween = _cube.RotateYTo(360, 4f, _context);
		sequence.Join(rotateTween);

		sequence.AppendInterval(1f);
		sequence.AppendCallback(() => GD.Print("Callback 1 invoke"));

		sequence.Join(_cube.ScaleTo(_cube.Scale * 0.25f, 1f, _context));

		sequence.AppendInterval(3f);
		sequence.AppendCallback(() => GD.Print("Callback 2 invoke"));

		var moveLeftTween = _cube.MoveXTo(_cube.GlobalPosition.X, 2f, _context);
		moveLeftTween.SetOnComplete(() => GD.Print("Second Tween is Completed"));
		sequence.Append(moveLeftTween);

		sequence.SetOnComplete(() => GD.Print("Sequence is Completed"));
	}

	public override void _Process(double delta)
	{
		_context.Update((float)delta);
	}
}
