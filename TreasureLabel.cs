using Godot;
using System;
using GameaWeekRogueLike.State;


public class TreasureLabel : Label
{
    private GameState gameState;

    public override void _Ready()
    {            
        gameState = GetTree().Root.GetNode("GameState") as GameState;
        Text = $"{gameState.Treasure}";

        gameState.Connect("UpdateGui", this, nameof(_on_UpdateGui));
    }

    public void _on_UpdateGui()
    {
        Text = $"{gameState.Treasure}";
    }
}
