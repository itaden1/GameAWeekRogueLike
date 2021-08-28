using Godot;
using System;
using GameaWeekRogueLike.Entities;

public class Boss : Enemy
{

    public new int HP = 100;
    public new int Attk = 1;


    [Signal]
    public delegate void BossDied();

    public override void _Ready()
    {
        base._Ready();
        NextPosition = Position;
    }
    public override void removeHP(int amount)
    {
        HP = HP - amount;
        if (HP <= 0 )
        {
            EmitSignal(nameof(BossDied));
            QueueFree();
        }
    }

}
