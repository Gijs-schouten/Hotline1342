using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex
{
    public class Camera
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

        public void Update(Vector2 position)
        {
            if (Input.KeyPressed(Keys.W))
                Zoom += 0.01f;
            else if (Input.KeyPressed(Keys.S))
                Zoom -= 0.01f;

            if (Input.KeyPressed(Keys.A))
                Rotation += 0.01f;
            else if (Input.KeyPressed(Keys.D))
                Rotation -= 0.01f;

            centre = position;
            transform = Matrix.CreateTranslation(new Vector3(position, 0)) *
                                                 Matrix.CreateRotationZ(Rotation) *
                                                 Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                                                 Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }
    }
}
