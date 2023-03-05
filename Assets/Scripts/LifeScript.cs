using EventArgs;
using System;
using UnityEngine;

public class LifeScript : MonoBehaviour 
{
    public event EventHandler<DamageEventArgs> OnDamage;

    public int maxHealth;
    public int health;
    public bool isVulnerable = true;

    public GameObject healingPrefab;


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

    public void Heal()
    {
        health = maxHealth;

        if(healingPrefab != null)
        {
            var effect = Instantiate(healingPrefab, transform.position, healingPrefab.transform.rotation);
            effect.transform.SetParent(transform);
            Destroy(effect, 5);
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
