using Godot;
using System;

public class GameOver : Control
{

    AudioStreamPlayer buttonClick;

    public override void _Ready()
    {
        Button exitButton = (Button)GetNode("AspectRatioContainer/VBoxContainer/Button");
        exitButton.Connect("pressed", this, nameof(_on_ExitbuttonPressed));

        buttonClick = (AudioStreamPlayer)GetNode("ClickEffect");
    }
    async public void _on_ExitbuttonPressed()
    {        
        buttonClick.Play();
        await ToSignal(buttonClick, "finished");
        GetTree().ChangeScene("res://TitleScreen.tscn");
    }
}
