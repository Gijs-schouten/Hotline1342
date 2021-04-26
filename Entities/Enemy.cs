﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Interfaces;
using System.Collections.Generic;
using System;
using System.Text;
using PadZex.Core;
using PadZex.Collision;
using PadZex.Scripts.Particle;
using PadZex.Scenes;

namespace PadZex
{
    public class Enemy : Entity, IDamagable
    {
        public Texture2D enemySprite;
        public Vector2 enemyVelocity;

		private int particleAmount = 50;
     
        public override void Initialize(ContentManager content)
        {
            enemySprite = content.Load<Texture2D>("sprites/enemySprite");
			Origin = new Vector2(enemySprite.Width / 2, enemySprite.Height / 2);

            enemyVelocity.X = 5f;
            enemyVelocity.Y = 5f;
            Depth = 1;
            Scale = 0.38f;

        }
        public override void Update(Time time)
        {
            /*Position += enemyVelocity; //Makes the enemies move.
            var randomSpeed = new Random();
            var enemyDirection = -1;
            //enemyPosition.X = MathHelper.Clamp(enemyPosition.X, 0, window.ClientBounds.Width - width);
            //enemyPosition.Y = MathHelper.Clamp(enemyPosition.Y, 0, window.ClientBounds.Height - height);
            //Makes it so that the enemies cant exit the gamescreen.
            if (Position.X >= 1080 - width || Position.X <= 0) 
            {
                enemyVelocity *= enemyDirection; //Enemies will bounce back in a random direction.
                enemyVelocity.Y = randomSpeed.Next(-5, 5);
                
            }

            if (Position.Y >= 720 - height || Position.Y <= 0) 
            {
                enemyVelocity *= enemyDirection; //Enemies will bounce back in a random direction.
                enemyVelocity.X = randomSpeed.Next(-5, 5);
            }
            */

        }

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {
            //Draws the enemy sprite.
            Draw(spriteBatch, enemySprite);
        }

        public override Shape CreateShape()
        {
            var shape = new Collision.Rectangle(this, Vector2.Zero, new Vector2(enemySprite.Width, enemySprite.Height));
            return shape;
        }

        public void Damage(Entity entity, float damage = 0)
        {
			if (damage > 0) Die();
        }

		private void Die() {
			PlayScene playScene = Scene.MainScene as PlayScene;
            if (playScene != null) playScene.EnemyCount--;
			SpawnBlood();
			Scene.MainScene.DeleteEntity(this);
		}

		private void SpawnBlood() {
			var particles = new BloodParticle[particleAmount];

			for (int i = 0; i < particleAmount; i++)
			{
				particles[i] = new BloodParticle(Position, false);
				Scene.MainScene.AddEntity(particles[i]);
			}
		}
    }

}