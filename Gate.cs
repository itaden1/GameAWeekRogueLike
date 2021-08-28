using Godot;
using System;

public class Gate : Sprite
{
    public override void _Ready()
    {
        Node boss = GetTree().Root.GetNode("World/Map/Boss");
        boss.Connect("BossDied", this, nameof(_on_BossDied));
    }
    public void _on_BossDied()
    {
        QueueFree();
    }
}
