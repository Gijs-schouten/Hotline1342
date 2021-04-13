﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PadZex.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadZex.Entities
{
    public class MouseEntity : Entity
    {
        private Texture2D texture;
        private Camera camera;

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {
            Draw(spriteBatch, texture);
        }

        public override void Initialize(ContentManager content)
        {
            texture = content.Load<Texture2D>("sprites/cursor");
            Origin = new Vector2(texture.Width, texture.Height) / 2;
            Scale = 4;
            Depth = 10;

            camera = FindEntity<Camera>("Camera");
        }

        public override void Update(Time time)
        {
            if(camera == null)
            {
                camera = FindEntity<Camera>("Camera");
                return;
            }
            Position = camera.MousePosition;
        }
    }
}
