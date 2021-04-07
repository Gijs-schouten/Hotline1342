using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PadZex.Core;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PadZex.Entities.Level
{
    public class DoorSpawn : Entity
    {
        private enum DoorDirection { Horizontal = 0, Vertical = 2 }
        private DoorDirection doorDirection;
        private Door door;

        public DoorSpawn(LevelLoader.Level level, Point gridPos)
        {
            var tiles = level.Tiles.ToList();
            if (IsSolid(tiles, level.Size, new Point(gridPos.X - 1, gridPos.Y), new Point(gridPos.X + 1, gridPos.Y)))
                doorDirection = DoorDirection.Vertical;
            else doorDirection = DoorDirection.Horizontal;
                
        }

        /// <summary>
        /// returns ture if all point positions are solid
        /// </summary>
        private bool IsSolid(List<LevelLoader.Tile> tiles, Point levelSize, params Point[] positions)
        {
            Func<Point, int> p = point => levelSize.X * point.X + point.Y;
            bool isSolid = true;

            for (int i = 0; i < positions.Length; i++)
            {
                int point = p(positions[i]);
                if (point < 0 || point >= tiles.Count) continue;
                isSolid = isSolid && tiles[i].Shape != null;
            }

            return isSolid;
        }

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {
            
        }

        public override void Initialize(ContentManager content)
        {
            door = new Door((short)doorDirection)
            {
                Position = Position
            };
            Scene.MainScene.AddEntity(door);
        }

        public override void Update(Time time)
        {

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Scene.MainScene.DeleteEntity(door);
        }
    }
}
