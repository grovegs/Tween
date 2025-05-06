using Godot;

public partial class Main : Node2D
{
    public override async void _Ready()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        GetTree().ChangeSceneToFile("res://TestScene.tscn");
    }
}