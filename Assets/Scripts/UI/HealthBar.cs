using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private int maxHealth;
    private int health;
    [SerializeField] private Slider slider;

    
    void Start() { }

    
    void Update()
    {
        slider.value = health;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        slider.maxValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

}
