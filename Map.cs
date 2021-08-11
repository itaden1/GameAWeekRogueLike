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
                if (grid[y,x] == 1 || grid[y,x] == 2 || grid[y,x] == 3)
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
                }
                if (grid[y,x] == 3)
                {
                    // goal
                    var treasureScene = (PackedScene)ResourceLoader.Load("res://Treasure.tscn");
                    Sprite treasure = (Sprite)treasureScene.Instance();
                    treasure.Position = new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize);
                    AddChild(treasure);

                }
            }
        }
        UpdateBitmaskRegion();
    }

}
