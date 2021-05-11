using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;

//Universele healthbar class. 
public class HealthBar
{
    private int health;
    private int maxHealth;
    private Vector2 position;

    private Rectangle healthBar;

    private Texture2D texture;
    private Vector2 offset;
    private int thickness;

    public HealthBar(Texture2D texture, int maxHealth, Vector2 offset, int thickness)
    {
        this.texture = texture;
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.offset = offset;
        this.thickness = thickness;

        healthBar = new Rectangle(0, 0, 0, 0);
    }

    public void SetHealh(int health)
    {
        this.health = health;
    }

    public void UpdatePosition(Vector2 position)
    {
        this.position.X = position.X;
        this.position.Y = position.Y;

        Update();
    }

    private void Update()
    {
        healthBar = new Rectangle((int)position.X - maxHealth + (int)offset.X, (int)position.Y + (int)offset.Y, health, thickness);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, healthBar, Color.White);
    }
}
