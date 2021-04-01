using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using PadZex.Core;

namespace PadZex.Entities.Level
{
    public class EnemySpawn : Entity
    {
        private Enemy enemy;

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {

        }

        public override void Initialize(ContentManager content)
        {
            enemy = new Enemy
            {
                Position = Position
            };
            Scene.MainScene.AddEntity(enemy);
        }

        public override void Update(Time time)
        {

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Scene.MainScene.DeleteEntity(enemy);
        }
    }
}
