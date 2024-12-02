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

		var moveUpTween = _cube.MoveLocalYTo(_cube.GlobalPosition.Y + 0.5f, 5f, _context);

		var moveRightTween = _cube.MoveXTo(_cube.GlobalPosition.X + 2f, 5f, _context);
		moveRightTween.SetOnComplete(() => GD.Print("First Tween is Completed"));
		moveRightTween.SetEase(EaseType.InBack);
		sequence.Append(in moveRightTween);

		var rotateTween = _cube.RotateYTo(360, 5f, _context);
		sequence.Join(in rotateTween);

		sequence.AppendInterval(1f);

		var moveLeftTween = _cube.MoveXTo(_cube.GlobalPosition.X, 5f, _context);
		moveLeftTween.SetOnComplete(() => GD.Print("Second Tween is Completed"));
		sequence.Append(in moveLeftTween);

		sequence.SetOnComplete(() => GD.Print("Sequence is Completed"));
    }

	public override void _Process(double delta)
	{
		_context.Update((float)delta);
	}
}
