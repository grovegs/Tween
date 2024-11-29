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
		_cube.MoveXTo(_cube.GlobalPosition.X + 2f, 5f, _context)
		.Ease(EaseType.InBack)
		.OnComplete(() => GD.Print("Completed"))
		.OnUpdate<float>(position => {
			GD.Print(position);
		});

		_cube.RotateYTo(360, 5f, _context);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_context.Update((float)delta);
	}
}
