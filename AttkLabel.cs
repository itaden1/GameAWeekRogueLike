using Godot;
using System;
using GameaWeekRogueLike.State;


public class AttkLabel : Label
{
    private GameState gameState;

    public override void _Ready()
    {            
        gameState = GetTree().Root.GetNode("GameState") as GameState;
        Text = $"{gameState.PlayerAttackPower}";

        gameState.Connect("UpdateGui", this, nameof(_on_UpdateGui));
    }

    public void _on_UpdateGui()
    {
        Text = $"{gameState.PlayerAttackPower}";
    }
}