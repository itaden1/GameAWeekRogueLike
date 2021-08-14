using Godot;
using System;
using GameaWeekRogueLike.Settings;

namespace GameaWeekRogueLike.Entities
{
    public class Player : Sprite
    {

        [Signal]
        public delegate void PlayerMoved();

        private Area2D _collisionAreaEast;
        private Area2D _collisionAreaSouth;
        private Area2D _collisionAreaWest;
        private Area2D _collisionAreaNorth;

        public Vector2 NextPosition;
        private bool _canMove = true;

        [Export]
        public float Speed = 150;

        public override void _Ready()
        {
            GD.Print("ready");
            _collisionAreaEast = (Area2D)GetNode("CollisionAreaEast");
            _collisionAreaSouth = (Area2D)GetNode("CollisionAreaSouth");
            _collisionAreaWest = (Area2D)GetNode("CollisionAreaWest");
            _collisionAreaNorth = (Area2D)GetNode("CollisionAreaNorth");

        }

        public override void _Process(float delta)
        {
            float distance = GlobalPosition.DistanceTo(NextPosition);
            Vector2 direction = (NextPosition - Position).Normalized();
            if (distance > 1.5)
            {
                _canMove = false;
                Vector2 motion = direction * Speed * delta;
                Position += motion;
            }
            else 
            {
                _canMove = true;
                Position = NextPosition;
            }
        }
        public override void _Input(InputEvent inputEvent)
        {
            if (inputEvent.IsActionPressed("moveRight") && _canMove)
            {
                if (_collisionAreaEast.GetOverlappingBodies().Count == 0)
                {
                    NextPosition = new Vector2(Position.x + GameSettings.TileSize, Position.y);
                }
                EmitSignal("PlayerMoved");
            }
            else if (inputEvent.IsActionPressed("moveUp") && _canMove)
            {
                if (_collisionAreaNorth.GetOverlappingBodies().Count == 0)
                {
                    NextPosition = new Vector2(Position.x, Position.y - GameSettings.TileSize);
                }
                EmitSignal("PlayerMoved");
            }
            else if (inputEvent.IsActionPressed("moveLeft") && _canMove)
            {
                if (_collisionAreaWest.GetOverlappingBodies().Count == 0)
                {
                    NextPosition = new Vector2(Position.x - GameSettings.TileSize, Position.y);
                }
                EmitSignal("PlayerMoved");
            }
            else if (inputEvent.IsActionPressed("moveDown") && _canMove)
            {
                if (_collisionAreaSouth.GetOverlappingBodies().Count == 0)
                {
                    NextPosition = new Vector2(Position.x, Position.y + GameSettings.TileSize);
                }
                EmitSignal("PlayerMoved");

            }
        }
        public void DestroySelf()
        {
            GD.Print("player destrucor called");
            QueueFree();
        }
    }
}

