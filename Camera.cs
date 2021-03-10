﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex
{
    public class Camera : Entity
    {
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }

        private Vector2 centre;
        private Viewport viewport;

        private float zoom = 1;
        private float rotation = 0;

        public float X
        {
            get { return centre.X; }
            set { centre.X = value; }
        }

        public float Y
        {
            get { return centre.Y; }
            set { centre.Y = value; }
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f)
                    zoom = 0.1f;
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;

        }


        public override void Initialize(ContentManager content)
        {
            AddTag("camera");
        }

        public override void Update(Time time)
        {

            centre = new Vector2(Position.X, Position.Y);
            transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)) *
                                                Matrix.CreateRotationZ(Rotation) *
                                                Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                                                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {

        }
    }
}
