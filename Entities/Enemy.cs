using Microsoft.Xna.Framework;
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
using System.Linq;

namespace PadZex
{
    public class Enemy : Entity, IDamagable
    {
        public Texture2D enemySprite;
        public Vector2 enemyVelocity;

        private Vector2 lastPosition;
        private float distanceToPlayer;
        private float engageRange = 1024f;
        private bool isEngaged = false;
        private float angelDeg;
        private float angleRad;
        private float maxVelocity = 512f;
        private int moveTimer = 0;
        private bool isMoving = false;

		private int particleAmount = 50;
        private Entity sound;
        private Entity player;

        public override void Initialize(ContentManager content)
        {
            enemySprite = content.Load<Texture2D>("sprites/enemySprite");
			Origin = new Vector2(enemySprite.Width / 2, enemySprite.Height / 2);

            Depth = 1;
            Scale = 0.38f;
            player = FindEntity("Player");
        }
        public override void Update(Time time)
        {
            Random r = new Random();
            if (!isMoving)
            {
                lastPosition = Position;
                isMoving = true;
                moveTimer = 300;

                angelDeg = r.Next(0, 360);                
                angleRad = Convert.ToSingle((Math.PI / 180) * angelDeg);
                enemyVelocity.X = Convert.ToSingle(Math.Cos(angleRad));
                enemyVelocity.Y = Convert.ToSingle(Math.Sin(angleRad));
            }
            else
            {
                if (moveTimer > 0)
                {
                    float xVelocity = enemyVelocity.X * maxVelocity * time.deltaTime;
                    float yVelocity = enemyVelocity.Y * maxVelocity * time.deltaTime;

                    Position.X += xVelocity;
                    CheckHorizontalCollision(xVelocity);

                    Position.Y += yVelocity;
                    CheckVerticalCollision(yVelocity);
                    moveTimer--;
                }
                else
                {
                    isMoving = false;
                }
            }

            
            distanceToPlayer = Convert.ToSingle(Math.Sqrt((Position.X - player.Position.X) * (Position.X - player.Position.X) + ((Position.Y - player.Position.Y) * (Position.Y - player.Position.Y))));
            if (distanceToPlayer < engageRange) isEngaged = true;
            attack();
        }

        private void attack()
        {
            if (isEngaged)
            {

            }
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
