using BaseProject.Scripts.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Collision;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex.Scripts.Weapons
{
    public class Weapon : Entity
    {
        public float WeaponDamage { get; set; }
        public float WeaponSpeed { get; set; }
        public float RotationSpeed { get; set; }
        public string SpriteLocation { get; set; }
        public bool Rotating { get; set; }
        public bool isFlipped { get; set; }
        public Vector2 Offset { get; set; }

		public bool throwing;
		public float velocity = 0;

		private Vector2 direction;
		private bool pickedUp, collidingWithPlayer = false;
        private Texture2D weaponSprite;
        private Entity player;
        
        public event Action<float> HitsObject;

        public override void Initialize(ContentManager content)
        {
            weaponSprite = content.Load<Texture2D>(SpriteLocation);
            player = FindEntity("Player");
            Position = new Vector2(50, 50);
            Origin = new Vector2(weaponSprite.Width / 2, weaponSprite.Height / 2);

            if (isFlipped)
            {
                FlipSprite();
            }

        }

        public override Shape InitializeShape()
        {
            var shape = new Collision.Circle(this, Vector2.Zero, weaponSprite.Width * Scale / 2);
            shape.ShapeEnteredEvent += CollisionEnter;
			shape.ShapeExitedEvent += CollisionExit;
            return shape;
        }

		public void ThrowWeapon()
        {
            velocity = 1;
            MouseState state = Mouse.GetState();
            Vector2 mousePos = new Vector2(state.X, state.Y);
            direction = mousePos - Position;
            throwing = true;
        }

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {
            Draw(spriteBatch, weaponSprite);
        }

        public override void Update(Time time)
        {
            KeyboardState state = Keyboard.GetState();

            if (!throwing && pickedUp)
            {
                Position = player.Position + Offset;
            }

            if (throwing)
            {
                if (Rotating)
                {
                    Angle += RotationSpeed * velocity * time.deltaTime;
                }
                else
                {
                    Angle = VectorToAngle(direction);
                }

                if (velocity > 0)
                {
                    velocity -= time.deltaTime;
                    Position += direction * WeaponSpeed * velocity * time.deltaTime;
                }

            }

            if (state.IsKeyDown(Keys.Space) && pickedUp)
            {
                ThrowWeapon();
                throwing = true;
                pickedUp = false;
            }

            if (state.IsKeyDown(Keys.F) && collidingWithPlayer)
            {
                PickUp();
            }
        }

        private float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public void PickUp()
        {
            pickedUp = true;
            throwing = false;
        }

        private void CollisionEnter(Entity entity)
        {
			(entity as IDamagable)?.Damage(this, WeaponDamage);

			if (entity is Player) collidingWithPlayer = true;

		}

		private void CollisionExit(Entity entity)
		{
			if (entity is Player) collidingWithPlayer = false;
		}

	}
}
