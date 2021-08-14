using Godot;
using System;

public class TitleScreen : Control
{
 
    public override void _Ready()
    {
        Button startButton = (Button)GetNode("MainMenuContainer/VSplitContainer/StartButton");
        Button quitButton = (Button)GetNode("MainMenuContainer/VSplitContainer/QuitButton");
        Button beginButton = (Button)GetNode("IntroDialogueContainer/BeginButton");
        startButton.Connect("pressed", this, nameof(_on_StartButton_Pressed));
        quitButton.Connect("pressed", this, nameof(_on_QuitButton_Pressed));
        beginButton.Connect("pressed", this, nameof(_on_BeginButtonPressed));
    }
    public void _on_StartButton_Pressed()
    {
        Container mainMenu = (Container)GetNode("MainMenuContainer");
        mainMenu.Visible = false;
        Container introDialogue = (Container)GetNode("IntroDialogueContainer");
        introDialogue.Visible = true;
    }
    public void _on_QuitButton_Pressed()
    {
        GetTree().Quit();
    }
    public void _on_BeginButtonPressed()
    {
        GetTree().ChangeScene("res://main.tscn");
    }

}
