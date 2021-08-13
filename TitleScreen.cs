using Godot;
using System;

public class TitleScreen : Control
{
 
    public override void _Ready()
    {
        Button startButton = (Button)GetNode("Panel/VSplitContainer/StartButton");
        Button quitButton = (Button)GetNode("Panel/VSplitContainer/QuitButton");
        startButton.Connect("pressed", this, nameof(_on_StartButton_Pressed));
        quitButton.Connect("pressed", this, nameof(_on_QuitButton_Pressed));
    }
    public void _on_StartButton_Pressed()
    {
        GetTree().ChangeScene("res://main.tscn");
    }
    public void _on_QuitButton_Pressed()
    {
        GetTree().Quit();
    }

}
