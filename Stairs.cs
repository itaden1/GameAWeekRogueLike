using Godot;
using System;
using GameaWeekRogueLike.State;


public class Stairs : Sprite
{
    [Signal]
    public delegate void StairsEntered();
    private GameState gameState;

    public override void _Ready()
    {
        Area2D area = (Area2D)GetNode("Area2D");
        gameState = GetTree().Root.GetNode("GameState") as GameState;
        area.Connect("area_entered", this, nameof(_on_BodyEnteredStair));   
    }

    public void _on_BodyEnteredStair(Area2D area)
    {
        if (area.Name == "PlayerHitBox")
        {
            if (gameState.Treasure >= 20)
            {
                GetTree().ChangeScene("res://BossFight.tscn");
            }
            else
            {
                EmitSignal("StairsEntered");
            }
        }
    }
}
