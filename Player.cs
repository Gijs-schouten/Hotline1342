using BaseProject.Scripts.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Collision;
using PadZex.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadZex
{
	/// <summary>
	///  Player with static sprite and hp bar. 
	///  WASD : Move
	/// </summary>
	public class Player : Entity
	{
		private Texture2D playerSprite;
		private Color color = Color.White;

		private Health health;
		private HealthBar healthbar;

		//Vector2 move;

		private float speed; 
		public override void Initialize(ContentManager content)
		{
			playerSprite = content.Load<Texture2D>("sprites/player");
			health = new Health(100, 100);
			healthbar = new HealthBar(content.Load<Texture2D>("sprites/HealthBar"));
			speed = 0; 
			AddTag("Player");
			Depth = 1;
		}

		public override void Draw(SpriteBatch spriteBatch, Time time)
		{ 
			Draw(spriteBatch, playerSprite);
			//healthbar.Draw(spriteBatch, Position, health.GetHealth);

			
		} 
		// Normalizering en acceleration Player
		// Gebruik gemaakt van het input systeem 
		public override void Update(Time time)
		{ 
			float horizontal = -Convert.ToSingle(Input.KeyPressed(Keys.A)) + Convert.ToSingle(Input.KeyPressed(Keys.D));
			float vertical = -Convert.ToSingle(Input.KeyPressed(Keys.W)) + Convert.ToSingle(Input.KeyPressed(Keys.S));

			Vector2 move = new Vector2(horizontal, vertical);
			
			if(move != new Vector2(0,0))
            { 
				speed += .4f;
				move = Vector2.Normalize(move);
			} 
            else 
			{ 
				speed = 0f;
			}
			
			speed = Math.Clamp(speed, 0, 10);
			Debug.Log(speed);
			Position += move * speed;
		}

		public override Shape InitializeShape()
		{
			var shape = new Collision.Rectangle(this, Vector2.Zero, new Vector2(playerSprite.Width, playerSprite.Height));
			shape.ShapeEnteredEvent += OnShapeEnteredEvent;
			shape.ShapeExitedEvent += OnShapeExitedEvent;
			return shape;
		} 

		private void OnShapeExitedEvent(Entity shape)
		{ 
			color = Color.White;
			Debug.Log("exited");
		} 
		//public Collision.Shape IntializeShape(Player player) 
		//{ 
		//	var shape = new Collision.Circle(this, Vector2.Zero, 2);  
		//	return shape;
		//} 
		// private void Damage(Entity entity, float damage = 0)
		//{ 
		//	Entity.DeleteEntity(this);
	// 	}  

		private void OnShapeEnteredEvent(Entity shape)
		{ 
			color = Color.Red;
			Debug.Log("entered");

			health.GetHit(5);
		}

    }
}
