using Godot;
using System;

public class GameOver : Control
{

    public override void _Ready()
    {
        Button exitButton = (Button)GetNode("AspectRatioContainer/VBoxContainer/Button");
        exitButton.Connect("pressed", this, nameof(_on_ExitbuttonPressed));
    }
    public void _on_ExitbuttonPressed()
    {
        GetTree().ChangeScene("res://TitleScreen.tscn");
    }
}
