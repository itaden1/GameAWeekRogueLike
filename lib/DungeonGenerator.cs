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
            while (rects.Count <= _roomCount)
            {
                // create a rect
                int width = _random.Next(_minRoomSize, _maxRoomSize);
                int height = _random.Next(_minRoomSize, _maxRoomSize);
                int positionX = _random.Next(0, _mapWidth - width);
                int positionY = _random.Next(0, _mapHeight - height);
                Rect2 rect = new Rect2(new Vector2(positionX, positionY), new Vector2(width, height));

                // check if it overlaps
                bool validPlacement = true;
                foreach(Rect2 r in rects)
                {
                    if (rect.Intersects(r))
                    {
                        validPlacement = false;
                    }
                }
                if (validPlacement)
                {
                    ClearArea(grid, rect);
                    // add corridoors
                    if (rects.Count == 1)
                    {
                        // join with corridoor
                        int startX = (int)Math.Min(rects[0].Position.x, rect.Position.x);
                        Rect2 r1 = new Rect2(startX, rect.Position.y, Math.Abs(
                            rects[0].Position.x - rect.Position.x
                        ), 3);
                        ClearArea(grid, r1);
                        rects.Add(r1);
                    }
                    else if (rects.Count >= 1)
                    {

                    }
                }
                // add it to array
                rects.Add(rect);
            }
            return grid;
        }
        private void ClearArea(int[,] grid, Rect2 area)
        {
            // clear an area on the grid
            for (int y = (int)area.Position.y; y < (int)area.End.y; y++)
            {
                for (int x = (int)area.Position.x; x < (int)area.End.x; x++)
                {
                    grid[y,x] = 1;
                }
            }
        }
    }
}
