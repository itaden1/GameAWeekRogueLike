using Godot;
using System;
using GameaWeekRogueLike.State;


public class Treasure : Sprite
{
    public int Value = 10;
    public override void _Ready()
    {
        Area2D area = (Area2D)GetNode("Area2D");
        area.Connect("area_entered", this, nameof(_on_BodyEnteredSTreasure));
    }

    public void _on_BodyEnteredSTreasure(Area2D area)
    {
        if (area.Name == "PlayerHitBox")
        {
            GameState gameState = GetTree().Root.GetNode("GameState") as GameState;
            gameState.UpdateTreasure(Value);
            QueueFree();
        }
    }
}
