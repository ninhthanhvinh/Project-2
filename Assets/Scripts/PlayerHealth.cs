using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float health;
    public float maxHealth;
    public Slider healthSlider;
    public void GetDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0 )
        {
            //Trigger death
        }
    }

    public void GetHeal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void Update()
    {
        healthSlider.value = health / maxHealth;
    }

}
