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
using PadZex.Entities;
using System.Linq;

namespace PadZex
{
    public class Enemy : Entity, IDamagable
    {
        private const float ENGAGE_RANGE = 1024f;
        private const float MAX_VELOCITY = 512f;

        public Texture2D enemySprite;
        public Vector2 enemyVelocity;

        private Vector2 lastPosition;
        private float distanceToPlayer;       
        private bool isEngaged = false;
        private float angelDeg;
        private float angleRad;        
        private float moveTimer = 0f;
        private bool isMoving = false;

		private int particleAmount = 50;
        private Entity sound;
        private Entity player;
        private EnemyWeapon weapon;

        public override void Initialize(ContentManager content)
        {
            enemySprite = content.Load<Texture2D>("sprites/enemySprite");
			Origin = new Vector2(enemySprite.Width / 2, enemySprite.Height / 2);
            weapon = new EnemyWeapon();
            Scene.MainScene.AddEntity(weapon);          
            Depth = 1;
            Scale = 0.38f;
            player = FindEntity("Player");
        }
        public override void Update(Time time)
        {
            if (!isMoving)
            {
                lastPosition = Position;
                isMoving = true;
                moveTimer = 3;

                angelDeg = CoreUtils.Random.Next(0, 360);                
                angleRad = (float)((Math.PI / 180) * angelDeg);
                enemyVelocity.X = (float)Math.Cos(angleRad);
                enemyVelocity.Y = (float)Math.Sin(angleRad);
            }
            else
            {
                if (moveTimer > 0)
                {
                    float xVelocity = enemyVelocity.X * MAX_VELOCITY * time.deltaTime;
                    float yVelocity = enemyVelocity.Y * MAX_VELOCITY * time.deltaTime;

                    Position.X += xVelocity;
                    CheckHorizontalCollision(xVelocity);

                    Position.Y += yVelocity;
                    CheckVerticalCollision(yVelocity);
                    moveTimer -= time.deltaTime;
                }
                else
                {
                    isMoving = false;
                }
            }

            
            distanceToPlayer = (float)Math.Sqrt((Position.X - player.Position.X) * (Position.X - player.Position.X) + ((Position.Y - player.Position.Y) * (Position.Y - player.Position.Y)));
            if (distanceToPlayer < ENGAGE_RANGE) isEngaged = true;
            Attack();
        }

        private void Attack()
        {
            if (isEngaged && !weapon.IsFlying) weapon.Reset(Position+(Origin*Scale));
        }

        private void CheckHorizontalCollision(float velocity)
        {
            (bool collided, IEnumerable<Shape> shapes) = Scene.MainScene.TestAllCollision(Shape);
            if (collided)
            {
                var walls = shapes.Where(x => x.Owner.Tags.Contains("wall")).Cast<Collision.Rectangle>();
                var wall = walls.FirstOrDefault();
                if (wall == null) return;

                if (velocity < 0) Position.X = wall.WorldX + wall.WorldWidth;
                else Position.X = wall.WorldX - ((Collision.Rectangle)Shape).WorldWidth;

            }
        }

        private void CheckVerticalCollision(float velocity)
        {
            (bool collided, IEnumerable<Shape> shapes) = Scene.MainScene.TestAllCollision(Shape);
            if (collided)
            {
                var walls = shapes.Where(x => x.Owner.Tags.Contains("wall")).Cast<Collision.Rectangle>();
                var wall = walls.FirstOrDefault();
                if (wall == null) return;

                if (velocity < 0) Position.Y = wall.WorldY + wall.WorldHeight;
                else Position.Y = wall.WorldY - ((Collision.Rectangle)Shape).WorldHeight;
            }
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
	        Sound.SoundPlayer.PlaySound(Sound.Sounds.ENEMY_HURT, this);
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
