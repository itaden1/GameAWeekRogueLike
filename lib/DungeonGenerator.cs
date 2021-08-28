using Godot;
using System;
using System.Collections.Generic;


namespace GameaWeekRogueLike.dungeonGeneration
{
    public class DungeonGenerator : Reference
    {
        private int _mapHeight;
        private int _roomCount;
        private int _maxRoomSize;
        private int _minRoomSize;
        private int _tileSize;
        private int _mapWidth;

        protected Random _random = new Random();

        public DungeonGenerator (
            int tileSize, 
            int mapWidth, 
            int mapHeight,
            int roomCount,
            int maxRoomSize,
            int minRoomSize)
        {
            _tileSize = tileSize;
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _roomCount = roomCount;
            _maxRoomSize = maxRoomSize;
            _minRoomSize = minRoomSize;
        }
        
        public int[,] GenerateGrid()
        {
            var grid = new int[_mapHeight, _mapWidth];
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    grid[y,x] = 0;
                }
            }

            var rects = new List<Rect2>();
            int threshold = _roomCount;
            while (rects.Count <= threshold)
            {
                // create a rect
                int width = _random.Next(_minRoomSize, _maxRoomSize);
                int height = _random.Next(_minRoomSize, _maxRoomSize);
                int positionX = _random.Next(0, _mapWidth - width);
                int positionY = _random.Next(0, _mapHeight - height);
                Rect2 rect = new Rect2(new Vector2(positionX, positionY), new Vector2(width, height));

                // check if it overlaps
                bool validPlacement = true;
                Rect2 rectBounds = rect.Grow(1);
                foreach(Rect2 r in rects)
                {
                    if (rectBounds.Intersects(r))
                    {
                        validPlacement = false;
                    }
                }
                if (validPlacement)
                {
                    ClearArea(grid, rect);
                    // add corridoors
                    if (rects.Count >= 1)
                    {
                        var corridoor = CreateCorridoor(rect, rects[rects.Count - 1]);
                        foreach(Rect2 r in corridoor)
                        {
                            ClearArea(grid, r);
                        }
                    }
                    // add it to array
                    rects.Add(rect);
                }
                threshold --;
            }
            AddTile(2, grid, rects[0]);
            AddTile(3, grid, rects[rects.Count - 1]);
            return grid;
        }
        private List<Rect2> CreateCorridoor(Rect2 r1, Rect2 r2)
        {
            Vector2 r1Mid = r1.End - r1.Size / 2;
            Vector2 r2Mid = r2.End - r2.Size / 2;

            // randomly move vertical or horizontal first
            if (_random.Next(1,2) == 1)
            {
                // horizontal
                var targetVec = new Vector2(r2Mid.x, r1Mid.y);
                int x1 = (int)Math.Min(r1Mid.x, targetVec.x);
                int x2 = (int)Math.Max(r1Mid.x, targetVec.x);
                int width = x2 - x1;
                int height = 3;
                Rect2 rect1 = new Rect2(x1 - 1, r1Mid.y, width + 3, height);
                
                // vertical
                int y1 = (int)Math.Min(targetVec.y, r2Mid.y);
                int y2 = (int)Math.Max(targetVec.y, r2Mid.y);
                int width2 = 3;
                int height2 = y2 - y1;
                Rect2 rect2 = new Rect2(targetVec.x, y1 - 1, width2, height2 + 3);
                return new List<Rect2>(){rect1, rect2};
            }
            else
            {
                // vertical
                var targetVec = new Vector2(r1Mid.x, r2Mid.y);
                int y1 = (int)Math.Min(r1Mid.y, targetVec.y);
                int y2 = (int)Math.Max(r1Mid.y, targetVec.y);
                int width = 3;
                int height = y2 - y1;
                Rect2 rect1 = new Rect2(r1Mid.x, y1 - 1, width, height + 3);

                // horrizontal
                int x1 = (int)Math.Min(targetVec.x, r2Mid.x);
                int x2 = (int)Math.Max(targetVec.x, r2Mid.x);
                int width2 = x2 - x1;
                int height2 = 3;
                Rect2 rect2 = new Rect2(x1 - 1, targetVec.y, width2 + 3, height2);
                return new List<Rect2>(){rect1, rect2};
            }
        }
        private void AddTile(int tile, int[,] grid, Rect2 rect)
        {
            var midRect = rect.End - rect.Size / 2;
            grid[(int)midRect.y, (int)midRect.x] = tile;
        }
        private void ClearArea(int[,] grid, Rect2 area)
        {
            // clear an area on the grid also randomly add an item or something
            for (int y = (int)area.Position.y; y < (int)area.End.y; y++)
            {
                for (int x = (int)area.Position.x; x < (int)area.End.x; x++)
                {
                    var vec = new Vector2(x,y);
                    if (vec.x > area.Position.x + 1
                        && vec.y > area.Position.y + 1
                        && vec.x < area.End.x -1
                        && vec.y < area.End.y - 1
                    )
                    {
                        AddEnemiesAndItems(grid, vec);
                    }
                    else
                    {
                        grid[(int)vec.y, (int)vec.x] = 1;
                    }

                }
            }
        }
        private void AddEnemiesAndItems(int[,] grid, Vector2 vec)
        {
            int roll = _random.Next(1, 100);
            if (roll >=93) grid[(int)vec.y, (int)vec.x] = 4; // enemy
            else grid[(int)vec.y, (int)vec.x] = 5;
        }
    }
}
