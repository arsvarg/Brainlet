using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    Rigidbody2D rb;

    public float p_currentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }


    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {

        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);

        if (currentHealth<=0)
        {
            Die();
        }
        
    }

    void Die()
    {
        rb.isKinematic = true;
        GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<GameManager>().StartCoroutine("Restart");
    }
}
