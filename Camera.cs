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
        
        private Viewport viewport;

        private float zoom = 1;
        private Entity target;

        public float X
        {
            get => Position.X;
            set => Position.X = value;
        }

        public float Y
        {
            get => Position.Y;
            set => Position.Y = value;
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

        public void SelectTarget(String targetString)
        {
            target = FindEntity(targetString);
        }

        public override void Initialize(ContentManager content)
        {
            AddTag("camera");
        }

        public override void Update(Time time)
        {
            Position = new Vector2(target.Position.X, target.Position.Y);
            transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                                Matrix.CreateRotationZ(Rotation) *
                                                Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                                                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }
        public override void Draw(SpriteBatch spriteBatch, Time time)
        {

        }
    }
}
