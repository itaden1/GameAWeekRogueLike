using Godot;
using System;
using GameaWeekRogueLike.State;

public class Potion : Sprite
{
    public int Value = 20;
    public override void _Ready()
    {
        Area2D area = (Area2D)GetNode("Area2D");
        area.Connect("area_entered", this, nameof(_on_BodyEnteredpotion));
    }

    public void _on_BodyEnteredpotion(Area2D area)
    {
        if (area.Name == "PlayerHitBox")
        {
            GameState gameState = GetTree().Root.GetNode("GameState") as GameState;
            gameState.UpdatePlayerHP(Value);
            QueueFree();
        }
    }
}
