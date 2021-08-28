using Godot;
using System;

public class Gate : Sprite
{
    public void Open(){
        QueueFree();
    }
}
