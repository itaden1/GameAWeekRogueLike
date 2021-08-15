using Godot;
using System;
using System.Collections.Generic;
using GameaWeekRogueLike.Settings;
using GameaWeekRogueLike.State;


namespace GameaWeekRogueLike.Entities
{
    public class Enemy : Sprite
    {
        [Export]
        public float Speed = 100;
        protected Random _random = new Random();
        enum Direction : int
        {
            North = 0,
            South = 1,
            East = 2,
            West = 3
        }
            private Area2D _collisionAreaEast;
            private Area2D _collisionAreaSouth;
            private Area2D _collisionAreaWest;
            private Area2D _collisionAreaNorth;

            public Vector2 NextPosition;
            private Player player;
            public int HP = 20;

            private Node gameState;
        public override void _Ready()
        {
            _collisionAreaEast = (Area2D)GetNode("CollisionAreaEast");
            _collisionAreaSouth = (Area2D)GetNode("CollisionAreaSouth");
            _collisionAreaWest = (Area2D)GetNode("CollisionAreaWest");
            _collisionAreaNorth = (Area2D)GetNode("CollisionAreaNorth");
            player = (Player)GetParent().GetNode("Player");

            player.Connect("PlayerMoved", this, nameof(_on_PlayerMoved));
            gameState = GetTree().Root.GetNode("GameState");
        }

        public void removeHP(int amount)
        {
            HP = HP - amount;
            if (HP <= 0 )
            {
                QueueFree();
            }
        }

        public override void _Process(float delta)
        {
            float distance = GlobalPosition.DistanceTo(NextPosition);
            Vector2 direction = (NextPosition - Position).Normalized();
            if (distance > 0.5)
            {
                Vector2 motion = direction * Speed * delta;
                Position += motion;
            }
            else 
            {
                Position = NextPosition;
            }
        }
        public void _on_PlayerMoved()
        {
            bool validChoice = false;
            bool attacking = false;
            while (!validChoice)
            {
                List<int> _choices = new List<int>();
                int playerDistance = (int)Position.DistanceTo(player.Position);
                if (playerDistance < 5 * GameSettings.TileSize)
                {
                    if (Position.x < player.NextPosition.x) _choices.Add(2);
                    if (Position.x > player.NextPosition.x) _choices.Add(3);
                    if (Position.y < player.NextPosition.y) _choices.Add(1);
                    if (Position.y > player.NextPosition.y) _choices.Add(0);
                }
                else 
                {
                    _choices.AddRange(new List<int>(){0,1,2,3});
                }
                int _rnd = _random.Next(0, _choices.Count - 1);
                int _choice = _choices[_rnd];

                if (_choice == (int)Direction.North)
                {
                    Godot.Collections.Array areas = _collisionAreaNorth.GetOverlappingAreas();
                    foreach (Area2D a in areas)
                    {
                        if (a.GetParent().Name == "Player")
                        {
                            GD.Print("Atack p");
                            NextPosition = new Vector2(Position.x, Position.y);
                            Position = new Vector2(Position.x, Position.y - GameSettings.TileSize/2);
                            attacking = true;
                            (gameState as GameState).UpdatePlayerHP(-5);
                            break;
                        }
                    }
                    if (_collisionAreaNorth.GetOverlappingBodies().Count == 0 && !attacking)
                    {
                        NextPosition = new Vector2(Position.x, Position.y - GameSettings.TileSize);
                        validChoice = true;
                    }
                }
                else if (_choice == (int)Direction.South)
                {
                    Godot.Collections.Array areas = _collisionAreaSouth.GetOverlappingAreas();
                    foreach (Area2D a in areas)
                    {
                        if (a.GetParent().Name == "Player")
                        {
                            GD.Print("Atack p");
                            NextPosition = new Vector2(Position.x, Position.y);
                            Position = new Vector2(Position.x, Position.y + GameSettings.TileSize/2);
                            attacking = true;
                            (gameState as GameState).UpdatePlayerHP(-5);
                            break;
                        }
                    }
                    if (_collisionAreaSouth.GetOverlappingBodies().Count == 0 && !attacking)
                    {
                        NextPosition = new Vector2(Position.x, Position.y + GameSettings.TileSize);
                        validChoice = true;
                    }
                }
                else if (_choice == (int)Direction.East)
                {
                    Godot.Collections.Array areas = _collisionAreaEast.GetOverlappingAreas();
                    foreach (Area2D a in areas)
                    {
                        if (a.GetParent().Name == "Player")
                        {
                            GD.Print("Atack p");
                            NextPosition = new Vector2(Position.x, Position.y);
                            Position = new Vector2(Position.x  + GameSettings.TileSize/2, Position.y);
                            attacking = true;
                            (gameState as GameState).UpdatePlayerHP(-5);
                            break;
                        }
                    }
                    if (_collisionAreaEast.GetOverlappingBodies().Count == 0 && !attacking)
                    {
                        NextPosition = new Vector2(Position.x + GameSettings.TileSize, Position.y);
                        validChoice = true;
                    }
                }
                else if (_choice == (int)Direction.West)
                {
                    Godot.Collections.Array areas = _collisionAreaWest.GetOverlappingAreas();
                    foreach (Area2D a in areas)
                    {
                        if (a.GetParent().Name == "Player")
                        {
                            GD.Print("Atack p");
                            NextPosition = new Vector2(Position.x, Position.y);
                            Position = new Vector2(Position.x - GameSettings.TileSize/2, Position.y);
                            (gameState as GameState).UpdatePlayerHP(-5);
                            attacking = true;
                            break;
                        }
                    }
                    if (_collisionAreaWest.GetOverlappingBodies().Count == 0 && !attacking)
                    {
                        NextPosition = new Vector2(Position.x - GameSettings.TileSize, Position.y);
                        validChoice = true;
                    }
                }
                break;
            }
        }
    }
}
