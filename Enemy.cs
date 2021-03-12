using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PadZex
{
    class Enemy
    {
        public Texture2D enemySprite;
        public Vector2 enemyPosition;
        public Vector2 enemySpeed;
        private GameWindow window;
        private int width = 40;
        private int height = 40;
        


        public void Initialize(ContentManager content, GameWindow window)
        {
            var random = new Random();
            enemySprite = content.Load<Texture2D>("sprites/enemySprite");
            
            enemyPosition.X = random.Next(0, window.ClientBounds.Width - width); //Enemies spawn at a random position.
            enemyPosition.Y = random.Next(0, window.ClientBounds.Height - height);

            enemySpeed.X = 5f;
            enemySpeed.Y = 5f;
            

            this.window = window;

        }

        public bool Update()
        {
            enemyPosition += enemySpeed; //Makes the enemies move.
            var randomSpeed = new Random();
            var enemyDirection = -1;
            //Console.WriteLine(enemyPosition.X);
            //Console.WriteLine(enemyPosition.Y);
            //enemyPosition.X = MathHelper.Clamp(enemyPosition.X, 0, window.ClientBounds.Width - width);
            //enemyPosition.Y = MathHelper.Clamp(enemyPosition.Y, 0, window.ClientBounds.Height - height);
            //Makes it so that the enemies cant exit the gamescreen.
            if (enemyPosition.X >= window.ClientBounds.Width - width || enemyPosition.X <= 40 - width) 
            {
                enemySpeed *= enemyDirection; //Enemies will bounce back in a random direction.
                enemySpeed.Y = randomSpeed.Next(-5, 5);
                
            }

            if (enemyPosition.Y >= window.ClientBounds.Height - height || enemyPosition.Y <= 40 - height) 
            {
                enemySpeed *= enemyDirection; //Enemies will bounce back in a random direction.
                enemySpeed.X = randomSpeed.Next(-5, 5);
            }
            return true;

        }



        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws the enemy sprite.
            spriteBatch.Draw(enemySprite, new Rectangle ((int)enemyPosition.X, (int)enemyPosition.Y, width, height), new Rectangle (0, 0, enemySprite.Width, enemySprite.Height), Color.White);
        }

       

    }

}
