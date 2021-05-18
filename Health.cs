using System;
using System.Collections.Generic;
using System.Text;

//Universele health class. 
public class Health
{
    public delegate void DiedHandler();
    public event DiedHandler HasDiedEvent;

    public delegate void HealthChangedHandler(int health);
    public event HealthChangedHandler HealthChangedEvent;

    private int maxHealth = 100;
    private int health = 100;

    public float GetHealth
    {
        get
        {
            return health;
        }
    }

    public Health(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }

    public void Hit(int damage)
    {
        health -= damage;

        HealthChangedEvent?.Invoke(health);

        if (health <= 0)
        {
            HasDiedEvent?.Invoke();
        }

    }
}
