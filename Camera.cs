using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex
{
    public class Camera : Entity
    {
        public Matrix Transform
        {
            get { return transform; }
        }
        private Matrix transform;
        
        private Vector2 centre;
        private Viewport viewport;

        private float zoom = 1;

        public float X
        {
            get => centre.X;
            set => centre.X = value;
        }

        public float Y
        {
            get => centre.Y;
            set => centre.Y = value;
        }

        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                if (zoom < 0.1f)
                    zoom = 0.1f;
            }
        }

        public float Rotation
        {
            get => Angle;
            set => Angle = value;
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
            Entity player = FindEntity("Player");
            Position = new Vector2(0, 0);
            centre = new Vector2(player.Position.X, player.Position.Y);
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
