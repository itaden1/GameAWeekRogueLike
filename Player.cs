using Godot;
using System;
using System.Collections.Generic;
using GameaWeekRogueLike.Settings;
using GameaWeekRogueLike.State;


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
        private Area2D _playerHitBox;

        private AudioStreamPlayer2D _attackSound;
        private AudioStreamPlayer2D _footStepSound;
        private AudioStreamPlayer2D _itemSound;

        public Vector2 NextPosition;
        private bool _canMove = true;

        [Export]
        public float Speed = 150;
        public GameState gameState;
        public override void _Ready()
        {
            GD.Print("ready");
            _collisionAreaEast = (Area2D)GetNode("CollisionAreaEast");
            _collisionAreaSouth = (Area2D)GetNode("CollisionAreaSouth");
            _collisionAreaWest = (Area2D)GetNode("CollisionAreaWest");
            _collisionAreaNorth = (Area2D)GetNode("CollisionAreaNorth");
            _playerHitBox = (Area2D)GetNode("PlayerHitBox");
            gameState = GetTree().Root.GetNode("GameState") as GameState;
            gameState.Connect("UpdateGui", this, nameof(_on_PickupCollected));

            _attackSound = (AudioStreamPlayer2D)GetNode("AttackSound");
            _footStepSound = (AudioStreamPlayer2D)GetNode("FootStepSound");
            _itemSound  = (AudioStreamPlayer2D)GetNode("ItemSound");

            NextPosition = Position;
        }

        private void _on_PickupCollected()
        {
            _itemSound.Play();
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
                Godot.Collections.Array areas = _collisionAreaEast.GetOverlappingAreas();
                List<Enemy> enemies = new List<Enemy>();
                foreach (Area2D a in areas)
                {
                    if (a.Name == "HitBox")
                    {
                        enemies.Add((Enemy)a.GetParent());
                    }
                }
                if (enemies.Count > 0)
                {
                    GD.Print("Atack");
                    _attackSound.Play();
                    NextPosition = new Vector2(Position.x, Position.y);
                    Position = new Vector2(Position.x + GameSettings.TileSize/2, Position.y);
                    enemies[0].removeHP(gameState.PlayerAttackPower);
                }
                else if (_collisionAreaEast.GetOverlappingBodies().Count == 0)
                {
                    _footStepSound.Play();
                    NextPosition = new Vector2(Position.x + GameSettings.TileSize, Position.y);
                }
                EmitSignal("PlayerMoved");
            }
            else if (inputEvent.IsActionPressed("moveUp") && _canMove)
            {
                Godot.Collections.Array areas = _collisionAreaNorth.GetOverlappingAreas();
                List<Enemy> enemies = new List<Enemy>();
                foreach (Area2D a in areas)
                {
                    if (a.Name == "HitBox")
                    {
                        enemies.Add((Enemy)a.GetParent());
                    }
                }
                if (enemies.Count > 0)
                {
                    GD.Print("Atack");
                    _attackSound.Play();
                    NextPosition = new Vector2(Position.x, Position.y);
                    Position = new Vector2(Position.x, Position.y - GameSettings.TileSize/2);
                    enemies[0].removeHP(gameState.PlayerAttackPower);
                }
                else if (_collisionAreaNorth.GetOverlappingBodies().Count == 0)
                {
                    _footStepSound.Play();
                    NextPosition = new Vector2(Position.x, Position.y - GameSettings.TileSize);
                }
                EmitSignal("PlayerMoved");
            }
            else if (inputEvent.IsActionPressed("moveLeft") && _canMove)
            {
                Godot.Collections.Array areas = _collisionAreaWest.GetOverlappingAreas();
                List<Enemy> enemies = new List<Enemy>();
                foreach (Area2D a in areas)
                {
                    if (a.Name == "HitBox")
                    {
                        enemies.Add((Enemy)a.GetParent());
                    }

                }
                if (enemies.Count > 0)
                {
                    GD.Print("Atack");
                    _attackSound.Play();
                    NextPosition = new Vector2(Position.x, Position.y);
                    Position = new Vector2(Position.x - GameSettings.TileSize/2, Position.y);
                    enemies[0].removeHP(gameState.PlayerAttackPower);
                }
                else if (_collisionAreaWest.GetOverlappingBodies().Count == 0)
                {
                    _footStepSound.Play();
                    NextPosition = new Vector2(Position.x - GameSettings.TileSize, Position.y);
                }
                EmitSignal("PlayerMoved");
            }
            else if (inputEvent.IsActionPressed("moveDown") && _canMove)
            {
                Godot.Collections.Array areas = _collisionAreaSouth.GetOverlappingAreas();
                List<Enemy> enemies = new List<Enemy>();
                foreach (Area2D a in areas)
                {
                    if (a.Name == "HitBox")
                    {
                        enemies.Add((Enemy)a.GetParent());
                    }
                }
                if (enemies.Count > 0)
                {
                    GD.Print("Atack");
                    _attackSound.Play();
                    NextPosition = new Vector2(Position.x, Position.y);
                    Position = new Vector2(Position.x, Position.y + GameSettings.TileSize/2);
                    enemies[0].removeHP(gameState.PlayerAttackPower);
                }
                else if (_collisionAreaSouth.GetOverlappingBodies().Count == 0)
                {
                    _footStepSound.Play();
                    NextPosition = new Vector2(Position.x, Position.y + GameSettings.TileSize);
                }
                EmitSignal("PlayerMoved");

            }
        }
    }
}

