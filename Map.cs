using Godot;
using System.Collections.Generic;
using GameaWeekRogueLike.Settings;
using GameaWeekRogueLike.dungeonGeneration;

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
        DungeonGenerator generator = new DungeonGenerator(
            GameSettings.TileSize,
            MapWidth,
            MapHeight,
            RoomCount,
            MaxRoomSize,
            MinRoomSize
        );

        int[,] grid = generator.GenerateGrid();
        GD.Print(grid);

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
                if (grid[y,x] == 2)
                {
                    var playerScene = (PackedScene)ResourceLoader.Load("res://Player.tscn");
                    Sprite player = (Sprite)playerScene.Instance();
                    player.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(player);
                    Camera2D camera = new Camera2D();
                    camera.Current = true;
                    player.AddChild(camera); 

                }
                if (grid[y,x] == 3)
                {
                    // goal
                    var treasureScene = (PackedScene)ResourceLoader.Load("res://Treasure.tscn");
                    Sprite treasure = (Sprite)treasureScene.Instance();
                    treasure.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(treasure);

                }
                if (grid[y,x] == 4)
                {
                    var enemyScene = (PackedScene)ResourceLoader.Load("res://Enemy.tscn");
                    Sprite enemy = (Sprite)enemyScene.Instance();
                    enemy.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(enemy);
                }
                if (grid[y,x] == 5)
                {
                    var jarScene = (PackedScene)ResourceLoader.Load("res://Jar.tscn");
                    Sprite jar = (Sprite)jarScene.Instance();
                    jar.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(jar);
                }
                if (grid[y,x] == 6)
                {
                    var powerUpScene = (PackedScene)ResourceLoader.Load("res://PowerUp.tscn");
                    Sprite powerUp = (Sprite)powerUpScene.Instance();
                    powerUp.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(powerUp);
                }
            }
        }
        UpdateBitmaskRegion();
    }

}
