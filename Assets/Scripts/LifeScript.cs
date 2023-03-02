using EventArgs;
using System;
using UnityEngine;

public class LifeScript : MonoBehaviour 
{
    public event EventHandler<DamageEventArgs> OnDamage;

    public int maxHealth;
    public int health;
    public bool isVulnerable = true;

    void Start() {
        health = maxHealth;
    }

    public void InflictDamage(GameObject attacker, int damage)
    {
        if (isVulnerable)
        {
            health -= damage;
            OnDamage?.Invoke(this, new DamageEventArgs
            {
                attacker = attacker,
                damage = damage
            });
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
