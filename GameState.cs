using GameaWeekRogueLike.Entities;
using Godot;
using System;


namespace GameaWeekRogueLike.State
{

    public class GameState : Node
    {
        [Signal]
        public delegate void UpdateGui();

        public int PlayerHealth = 100;
        public int PlayerAttackPower = 5;
        public int Treasure = 0;

        public override void _Ready()
        {
            
        }
        public void UpdatePlayerHP(int amount)
        {
            PlayerHealth = PlayerHealth + amount;
            if (PlayerHealth > 100)
            {
                PlayerHealth = 100;
            }
            EmitSignal("UpdateGui");
            if (PlayerHealth <= 0)
            {
                PlayerHealth = 100;
                PlayerAttackPower = 5;
                Treasure = 0;
                GetTree().ChangeScene("res://GameOver.tscn");
            }

        }
        public void updatPlayerAttk(int amount)
        {
            PlayerAttackPower = PlayerAttackPower + amount;
            EmitSignal("UpdateGui");
        }
        public void UpdateTreasure(int amount)
        {
            Treasure += amount;
            EmitSignal("UpdateGui");
            if (Treasure >= 200)
            {
                PlayerHealth = 100;
                PlayerAttackPower = 5;
                Treasure = 0;
                GetTree().ChangeScene("res://YouWinScreen.tscn");
            }
        }
    }
}

