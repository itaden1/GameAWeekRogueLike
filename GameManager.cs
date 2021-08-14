using Godot;
using System;

public class GameManager : Node
{
    [Signal]
    public delegate void PlayerMoved();
    
    [Signal]
    public delegate void EnemyMoved();

    private bool _playerTurn = true;
    private bool _enemyTurn = false;
    public bool PlayerTurn
    {
        get => _playerTurn;
        set
        {
            _playerTurn = value;
            _enemyTurn = !value;
            EmitSignal("PlayerMoved");       
        }
    }
    public bool EnemyTurn
    {
        get => _enemyTurn;
        set
        {
            _enemyTurn = value;
            _playerTurn = !value;
            EmitSignal("EnemyMoved");       
        }
    }
    public override void _Ready()
    {
        
    }

}
