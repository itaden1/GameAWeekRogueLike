using Godot;
using System;


public class BaseEntity : Sprite
{

    public override void _Ready()
    {
        
    }

    public void DestroySelf()
    {
        GD.Print("destrucor called");
        Free();
    }
}
