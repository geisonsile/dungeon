using EventArgs;
using System;
using UnityEngine;

public class LifeScript : MonoBehaviour 
{
    public event EventHandler<DamageEventArgs> OnDamage;
    public int maxHealth;
    public int health;

    void Start() {
        health = maxHealth;
    }

    public void InflictDamage(GameObject attacker, int damage)
    {
        health -= damage;
        OnDamage?.Invoke(this, new DamageEventArgs { 
            attacker = attacker,
            damage = damage
        });
    }
}
