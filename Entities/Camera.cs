using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using PadZex.Core;

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

        private float zoom = 0.5f;
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

        public Vector2 MousePosition => Input.MousePosition.ToVector2() / Zoom + Position;
		public Vector2 GlobalPosition => Position;
		
		public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void SelectTarget(String targetString)
        {
            target = FindEntity(targetString);
            Origin = CoreUtils.Point.ToVector2() / 2;
        }

        public override void Initialize(ContentManager content)
        {
            AddTag("Camera");
        }

        public override void Update(Time time)
        {
            if (target != null) Position = new Vector2(target.Position.X, target.Position.Y) - Origin / Zoom;
 			transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
													Matrix.CreateRotationZ(Rotation) *
													Matrix.CreateScale(new Vector3(Zoom, Zoom, 0));
        }

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {

        }

		public bool IsInScreen(Entity obj) {
			if (obj.Position.X < GlobalPosition.X + CoreUtils.Point.X / Zoom &&
				obj.Position.X > GlobalPosition.X &&
				obj.Position.Y < GlobalPosition.Y + CoreUtils.Point.Y / Zoom &&
				obj.Position.Y > GlobalPosition.Y)
			{
				return true;
			}
			return false;
		}

		private void ScreenShake() {
			Random r = Core.CoreUtils.Random;
			Position += new Vector2(r.Next(-5, 5), r.Next(-5, 5));
		}
    }
}
