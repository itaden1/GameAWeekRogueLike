using Godot;
using System;

public class PowerUp : Sprite
{
    public void DestroySelf()
    {
        GD.Print("destrucor called");
        QueueFree();
    }
}
