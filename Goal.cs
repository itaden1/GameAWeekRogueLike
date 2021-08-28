using Godot;
using System;

public class Goal : Sprite
{
    public override void _Ready()
    {
        Area2D area = (Area2D)GetNode("Area2D");
        area.Connect("area_entered", this, nameof(_on_BodyEnteredpotion));
    }

    public void _on_BodyEnteredpotion(Area2D area)
    {
        if (area.Name == "PlayerHitBox")
        {
                GetTree().ChangeScene("res://YouWinScreen.tscn");
        }
    }
}
