using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Interfaces;
using PadZex.Collision;
using System;
using System.Diagnostics;

namespace PadZex.Weapons
{
	/// <summary>
	/// Base class for all weapons used by the player
	/// </summary>
	public class Weapon : Entity
	{
		/// <summary>
		/// Weapon settings set in the sub classes
		/// </summary>
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


		public override void Initialize(ContentManager content)
		{
			weaponSprite = content.Load<Texture2D>(SpriteLocation);
			player = FindEntity("Player");
			Origin = new Vector2(weaponSprite.Width / 2, weaponSprite.Height / 2);

			if (isFlipped)
			{
				FlipSprite();
			}

		}

		public override Shape CreateShape()
		{
			var shape = new Collision.Circle(this, Vector2.Zero, weaponSprite.Width * Scale / 2);
			shape.ShapeEnteredEvent += CollisionEnter;
			shape.ShapeExitedEvent += CollisionExit;
			return shape;
		}

		/// <summary>
		/// Funtion to throw your weapon to the mouse position
		/// </summary>
		public void ThrowWeapon()
		{
			player = FindEntity("Player");
			velocity = 1;
			MouseState state = Mouse.GetState();
			Vector2 mousePos = new Vector2(state.X, state.Y);
			direction = mousePos - Position + FindEntity("camera").Position;
			Debug.WriteLine($"{direction} = ( {mousePos} - {Position} - {FindEntity("camera").Position} )");
			Angle = VectorToAngle(direction);
			direction.Normalize();
			Debug.WriteLine($"{direction}   {Angle}");
			throwing = true;
			pickedUp = false;
		}

		public override void Draw(SpriteBatch spriteBatch, Time time)
		{
			Draw(spriteBatch, weaponSprite);
		}

		public override void Update(Time time)
		{
			KeyboardState state = Keyboard.GetState();

			//Weapon is attached to player if picked up
			if (!throwing && pickedUp)
			{
				Position = player.Position + Offset;
			}

			//If set, rotates the weapon and moves it to the destination
			if (throwing)
			{
				if (Rotating)
				{
					Angle += RotationSpeed * velocity * time.deltaTime;
				}
				else
				{
					//Angle = VectorToAngle(direction);
				}

				if (velocity > 0)
				{
					velocity -= time.deltaTime;
					Position += direction * WeaponSpeed * velocity * time.deltaTime;
				}

			}

			//Throws the weapon
			if (state.IsKeyDown(Keys.Space) && pickedUp)
			{
				ThrowWeapon();

			}

			//Picks op weapon if colliding with 'F'
			if (state.IsKeyDown(Keys.F) && collidingWithPlayer)
			{
				PickUp();
			}
		}

		/// <summary>
		/// Converts a vector to angle used for throwing the weapon in a straight line
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		private float VectorToAngle(Vector2 vector)
		{
			return (float)Math.Atan2(vector.Y, vector.X);
		}

		/// <summary>
		/// Picks up the weapon
		/// </summary>
		public void PickUp()
		{
			pickedUp = true;
			throwing = false;
		}

		/// <summary>
		/// Triggers on collision enter event
		/// Damages any IDamagable and tracks if player makes collison for PickUp()
		/// </summary>
		/// <param name="entity"></param>
		private void CollisionEnter(Entity entity)
		{
			(entity as IDamagable)?.Damage(this, WeaponDamage);

			if (entity is Player) collidingWithPlayer = true;

		}

		/// <summary>
		/// Triggers on colision exit event
		/// Sets player collision to false
		/// </summary>
		/// <param name="entity"></param>
		private void CollisionExit(Entity entity)
		{
			if (entity is Player) collidingWithPlayer = false;
		}

	}
}
