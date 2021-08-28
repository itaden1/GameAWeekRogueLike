using Godot;
using System;
using GameaWeekRogueLike.State;

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
        GameState gameState = GetTree().Root.GetNode("GameState") as GameState;
        gameState.PlayerHealth = 100;
        gameState.PlayerAttackPower = 5;
        gameState.Treasure = 0;
        buttonClick.Play();
        await ToSignal(buttonClick, "finished");
        GetTree().ChangeScene("res://TitleScreen.tscn");
    }
}
