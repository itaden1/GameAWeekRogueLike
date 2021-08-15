using Godot;
using System;

public class Stairs : Sprite
{
    [Signal]
    public delegate void StairsEntered();
    public override void _Ready()
    {
        Area2D area = (Area2D)GetNode("Area2D");
        area.Connect("area_entered", this, nameof(_on_BodyEnteredStair));   
    }

    public void _on_BodyEnteredStair(Area2D area)
    {
        if (area.Name == "PlayerHitBox")
        {
            EmitSignal("StairsEntered");
        }
    }
}
