using Godot;
using System;
using GameaWeekRogueLike.State;

public class PowerUp : Sprite
{
    public int Value = 1;
    public override void _Ready()
    {
        Area2D area = (Area2D)GetNode("Area2D");
        area.Connect("area_entered", this, nameof(_on_BodyEnteredSPowerUp));
    }

    public void _on_BodyEnteredSPowerUp(Area2D area)
    {
        if (area.Name == "PlayerHitBox")
        {
            GameState gameState = GetTree().Root.GetNode("GameState") as GameState;
            gameState.updatPlayerAttk(Value);
            QueueFree();
        }
    }
}
