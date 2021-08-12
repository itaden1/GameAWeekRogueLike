using Godot;
using System;

public class TitleScreen : Control
{
 
    public override void _Ready()
    {
        Button startButton = (Button)GetNode("Panel/VSplitContainer/StartButton");
        startButton.Connect("pressed", this, nameof(_on_StartButton_Pressed));
    }
    public void _on_StartButton_Pressed()
    {
        GetTree().ChangeScene("res://main.tscn");
    }

}
