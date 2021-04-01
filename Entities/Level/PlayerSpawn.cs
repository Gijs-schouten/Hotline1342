using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using PadZex.Core;

namespace PadZex.Entities.Level
{
    public class PlayerSpawn : Entity
    {
        public override void Draw(SpriteBatch spriteBatch, Time time)
        {

        }

        public override void Initialize(ContentManager content)
        {
            Debug.Log("hello");
            FindEntity("Player").Position = Position;
        }

        public override void Update(Time time)
        {

        }
    }
}
