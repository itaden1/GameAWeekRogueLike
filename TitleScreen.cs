using Godot;
using System;

public class TitleScreen : Control
{
    AudioStreamPlayer buttonClick;

    public override void _Ready()
    {
        Button startButton = (Button)GetNode("MainMenuContainer/VSplitContainer/StartButton");
        Button quitButton = (Button)GetNode("MainMenuContainer/VSplitContainer/QuitButton");
        Button beginButton = (Button)GetNode("IntroDialogueContainer/VBoxContainer/BeginButton");

        buttonClick = (AudioStreamPlayer)GetNode("ClickEffect");

        startButton.Connect("pressed", this, nameof(_on_StartButton_Pressed));
        quitButton.Connect("pressed", this, nameof(_on_QuitButton_Pressed));
        beginButton.Connect("pressed", this, nameof(_on_BeginButtonPressed));
    }
    public void _on_StartButton_Pressed()
    {
        buttonClick.Play();
        Container mainMenu = (Container)GetNode("MainMenuContainer");
        mainMenu.Visible = false;
        Container introDialogue = (Container)GetNode("IntroDialogueContainer");
        introDialogue.Visible = true;
    }
    public void _on_QuitButton_Pressed()
    {        
        buttonClick.Play();
        GetTree().Quit();
    }
    async public void _on_BeginButtonPressed()
    {
        buttonClick.Play();
        await ToSignal(buttonClick, "finished");
        GetTree().ChangeScene("res://main.tscn");
    }
}
