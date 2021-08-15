using Godot;
using System.Collections.Generic;
using GameaWeekRogueLike.Settings;
using GameaWeekRogueLike.dungeonGeneration;
using GameaWeekRogueLike.Entities;
using System;

public class Map : TileMap
{
    [Export]
    public int MapWidth;
    [Export]
    public int MapHeight;
    [Export]
    public int RoomCount;
    [Export]
    public int MaxRoomSize;
    [Export]
    public int MinRoomSize;

    public override void _Ready()
    {
        StartArea();
    }
    public void CreateMap()
    {
        Visible = true;
        DungeonGenerator generator = new DungeonGenerator(
            GameSettings.TileSize,
            MapWidth,
            MapHeight,
            RoomCount,
            MaxRoomSize,
            MinRoomSize
        );

        int[,] grid = generator.GenerateGrid();

        List<Vector2> enemyPositions = new List<Vector2>();

        int tile = TileSet.FindTileByName("autotileset");

        var cellArray = new List<Vector2>();

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y,x] >= 1)
                {
                    SetCell(x, y, tile);
                    cellArray.Add(new Vector2(x, y));
                }

                if (grid[y,x] == 3)
                {
                    // goal
                    var stairScene = (PackedScene)ResourceLoader.Load("res://Stairs.tscn");
                    Sprite stair = (Sprite)stairScene.Instance();
                    stair.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    stair.Connect("StairsEntered", this, nameof(_on_StairEntered));
                    AddChild(stair);
                    stair.AddToGroup("ItemGroup");
                }

                if (grid[y,x] == 2)
                {
                    var playerScene = (PackedScene)ResourceLoader.Load("res://Player.tscn");
                    Player player = (Player)playerScene.Instance();
                    player.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    player.Name = "Player";
                    AddChild(player);
                    player.NextPosition = player.Position;
                    Camera2D camera = new Camera2D();
                    camera.Current = true;
                    player.AddChild(camera);
                    player.AddToGroup("PlayerGroup");
                }


                if (grid[y,x] == 4)
                {
                    enemyPositions.Add(new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize));
                }

                if (grid[y,x] == 5)
                {
                    var treasureScene = (PackedScene)ResourceLoader.Load("res://Treasure.tscn");
                    Sprite treasure = (Sprite)treasureScene.Instance();
                    treasure.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(treasure);
                    treasure.AddToGroup("ItemGroup");
                }

                if (grid[y,x] == 6)
                {
                    var powerUpScene = (PackedScene)ResourceLoader.Load("res://PowerUp.tscn");
                    Sprite powerUp = (Sprite)powerUpScene.Instance();
                    powerUp.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(powerUp);
                    powerUp.AddToGroup("ItemGroup");
                }
                if (grid[y,x] == 7)
                {
                    var powerUpScene = (PackedScene)ResourceLoader.Load("res://Potion.tscn");
                    Sprite powerUp = (Sprite)powerUpScene.Instance();
                    powerUp.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(powerUp);
                    powerUp.AddToGroup("ItemGroup");
                }
            }
        }
        UpdateBitmaskRegion();
        SpawnEnemies(enemyPositions);
    }
    public void StartArea()
    {
        // destroy all enemies and pickups if they exist
        foreach (Node n in GetChildren())
        {
            n.QueueFree();
        }

        // clear tilemap
        Clear();
        // slightly bigger dungeon
        RoomCount += 5;

        // transition
        Visible = false;
        Timer timer = new Timer();
        timer.WaitTime = 0.2F;
        timer.OneShot = true;
        AddChild(timer);
        timer.Connect("timeout", this, nameof(CreateMap));
        timer.Start();
    }
    private void _on_StairEntered()
    {
        StartArea();
    }

    public void SpawnEnemies(List<Vector2> enemyPositions)
    {
        foreach(Vector2 pos in enemyPositions)
        {
            var enemyScene = (PackedScene)ResourceLoader.Load("res://Enemy.tscn");
            Enemy enemy = (Enemy)enemyScene.Instance();

            enemy.Position = new Vector2(pos);
            AddChild(enemy);
            enemy.NextPosition = new Vector2(pos);

            enemy.AddToGroup("EnemyGroup");
        }
    }

}
