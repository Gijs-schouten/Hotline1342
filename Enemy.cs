using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PadZex.Interfaces;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PadZex
{
    class Enemy : Entity, IDamagable
    {
        public Texture2D enemySprite;
        public Vector2 enemyPosition;
        public Vector2 enemySpeed;
        GameWindow window;
        private int width = 70;
        private int height = 70;

     
        public override void Initialize(ContentManager content)
        {
            
            var random = new Random();
            enemySprite = content.Load<Texture2D>("sprites/enemySprite");

            enemyPosition.X = random.Next(width, 1080 - width);//Enemies spawn at a random position.
            enemyPosition.Y = random.Next(height, 720 - height);

            enemySpeed.X = 5f;
            enemySpeed.Y = 5f;

        }
        public override void Update(Time time)
        {
            enemyPosition += enemySpeed; //Makes the enemies move.
            var randomSpeed = new Random();
            var enemyDirection = -1;
            //Console.WriteLine(enemyPosition.X);
            //Console.WriteLine(enemyPosition.Y);
            //enemyPosition.X = MathHelper.Clamp(enemyPosition.X, 0, window.ClientBounds.Width - width);
            //enemyPosition.Y = MathHelper.Clamp(enemyPosition.Y, 0, window.ClientBounds.Height - height);
            //Makes it so that the enemies cant exit the gamescreen.
            if (enemyPosition.X >= 1080 - width || enemyPosition.X <= 0) 
            {
                enemySpeed *= enemyDirection; //Enemies will bounce back in a random direction.
                enemySpeed.Y = randomSpeed.Next(-5, 5);
                
            }

            if (enemyPosition.Y >= 720 - height || enemyPosition.Y <= 0) 
            {
                enemySpeed *= enemyDirection; //Enemies will bounce back in a random direction.
                enemySpeed.X = randomSpeed.Next(-5, 5);
            }
            

        }

        public override void Draw(SpriteBatch spriteBatch, Time time)
        {
            //Draws the enemy sprite.
            spriteBatch.Draw(enemySprite, new Rectangle ((int)enemyPosition.X, (int)enemyPosition.Y, width, height), new Rectangle (0, 0, enemySprite.Width, enemySprite.Height), Color.White);
        }
        public Collision.Shape InitializeShape(Player player)
        {
            var shape = new Collision.Circle(this, Vector2.Zero, width / 2);
            return shape;
        }

        public void Damage(Entity entity, float damage = 0)
        {
            Entity.DeleteEntity(this);
        }
        
    }

}
