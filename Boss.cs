using Godot;
using System;
using GameaWeekRogueLike.Entities;

public class Boss : Enemy
{

    public new int HP = 200;

    [Export]
    public IDoor Gate;

    public override void _Ready()
    {
        base._Ready();
        NextPosition = Position;
    }
    public new void removeHP(int amount)
    {
        HP = HP - amount;
        if (HP <= 0 )
        {
            Gate.Open();
            QueueFree();
        }
    }

}
