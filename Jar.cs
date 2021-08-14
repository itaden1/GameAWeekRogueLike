using Godot;
using System;

public class Jar : Sprite
{
    public void DestroySelf()
    {
        GD.Print("destrucor called");
        QueueFree();
    }
}
