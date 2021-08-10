using Godot;
using System;
using GameaWeekRogueLike.Settings;

namespace GameaWeekRogueLike.Entities
{
    public class Player : Sprite
    {
        private GameSettings settings;
        private Area2D _collisionAreaEast;
        private Area2D _collisionAreaSouth;
        private Area2D _collisionAreaWest;
        private Area2D _collisionAreaNorth;

        public override void _Ready()
        {
            GD.Print("ready");
            _collisionAreaEast = (Area2D)GetNode("CollisionAreaEast");
            _collisionAreaSouth = (Area2D)GetNode("CollisionAreaSouth");
            _collisionAreaWest = (Area2D)GetNode("CollisionAreaWest");
            _collisionAreaNorth = (Area2D)GetNode("CollisionAreaNorth");

        }
        public override void _Input(InputEvent inputEvent)
        {
            if (inputEvent.IsActionPressed("moveRight"))
            {
                if (_collisionAreaEast.GetOverlappingBodies().Count == 0)
                {
                    Position = new Vector2(Position.x + GameSettings.TileSize, Position.y);
                }
            }
            else if (inputEvent.IsActionPressed("moveUp"))
            {
                if (_collisionAreaNorth.GetOverlappingBodies().Count == 0)
                {
                    Position = new Vector2(Position.x, Position.y - GameSettings.TileSize);
                }
            }
            else if (inputEvent.IsActionPressed("moveLeft"))
            {
                if (_collisionAreaWest.GetOverlappingBodies().Count == 0)
                {
                    Position = new Vector2(Position.x - GameSettings.TileSize, Position.y);
                }
            }
            else if (inputEvent.IsActionPressed("moveDown"))
            {
                if (_collisionAreaSouth.GetOverlappingBodies().Count == 0)
                {
                    Position = new Vector2(Position.x, Position.y + GameSettings.TileSize);
                }
            }
        }
    }
}

