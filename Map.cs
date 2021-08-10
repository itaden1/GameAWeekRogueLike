using Godot;
using System;
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

        var tile = TileSet.FindTileByName("autotileset");

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y,x] == 1)
                {
                    SetCell(x, y, tile);
                }
            }
        }
        UpdateBitmaskRegion();
    }

}
