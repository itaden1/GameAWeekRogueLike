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
    protected Random _random = new Random();

    [Export]
    public int maxTreasure = 3;
    [Export]
    public int maxPotions = 4;
    [Export]
    public int maxPowerups = 3;


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
        List<Vector2> tiles = new List<Vector2>();

        var cellArray = new List<Vector2>();

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y,x] >= 1)
                {
                    SetCell(x, y, tile);
                    cellArray.Add(new Vector2(x, y));
                    if (grid[y,x] == 5)
                    {
                        tiles.Add(new Vector2(x,y));
                    }
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
            }
        }
        UpdateBitmaskRegion();
        SpawnEnemies(enemyPositions);
        PlaceItems(tiles);
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

    public void PlaceItems(List<Vector2> tilePositions)
    {
        List<Vector2> takenPositions = new List<Vector2>();
        int treasureCount = _random.Next(0, maxTreasure);
        int potionCount = _random.Next(0, maxPotions);
        int swordCount = _random.Next(0, maxPowerups);

        takenPositions = PlaceItem("res://Treasure.tscn", tilePositions, takenPositions, treasureCount);
        takenPositions = PlaceItem("res://Potion.tscn", tilePositions, takenPositions, potionCount);
        takenPositions = PlaceItem("res://PowerUp.tscn", tilePositions, takenPositions, swordCount);
    }

    private List<Vector2> PlaceItem(string itemScenePath, List<Vector2> positions, List<Vector2> takenPositions, int count)
    {      
        while (count > 0)
        {
            // pick random tile
            Vector2 tile = positions[_random.Next(0, positions.Count)];
            if (!takenPositions.Contains(tile))
            {
                var scene = (PackedScene)ResourceLoader.Load(itemScenePath);
                Sprite instance = (Sprite)scene.Instance();
                instance.Position = new Vector2(tile.x * GameSettings.TileSize, tile.y * GameSettings.TileSize);
                AddChild(instance);
                instance.AddToGroup("ItemGroup");
                takenPositions.Add(tile);
                count --;
            }
        }
        return takenPositions;

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
