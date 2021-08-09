using Godot;
using System;
using GameaWeekRogueLike.Settings;

namespace GameaWeekRogueLike.Entities
{
    public class Player : Sprite
    {
        private GameSettings settings;
        public override void _Ready()
        {
            GD.Print("ready");
        }
        public override void _Input(InputEvent inputEvent)
        {
            if (inputEvent.IsActionPressed("moveRight"))
            {
                Position = new Vector2(Position.x + GameSettings.TileSize, Position.y);
            }
            else if (inputEvent.IsActionPressed("moveUp"))
            {
                Position = new Vector2(Position.x, Position.y - GameSettings.TileSize);
            }
            else if (inputEvent.IsActionPressed("moveLeft"))
            {
                Position = new Vector2(Position.x - GameSettings.TileSize, Position.y);
            }
            else if (inputEvent.IsActionPressed("moveDown")){
                Position = new Vector2(Position.x, Position.y + GameSettings.TileSize);
            }
        }
    }
}

