using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    float currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth<=0)
        {
            Die();
        }
        
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
