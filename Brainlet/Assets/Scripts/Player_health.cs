﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    public float maxHealth;
    static float currentHealth;
    Rigidbody2D rb;
    [SerializeField] GameObject particleEffectDeath;


    bool isDead;

    public float p_currentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }


     void Update()
    {
               
    }


    void Start()
    {
        
        FindObjectOfType<HealthBar>().SetHealth(currentHealth);
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            FindObjectOfType<AudioManager>().Play("damage");
        }
        
        StartCoroutine(Flash());
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        FindObjectOfType<HealthBar>().SetHealth(currentHealth);

        if (currentHealth<=0 && isDead == false)
        {
            Die();
        }
        
    }

    void Die()
    {
        Instantiate(particleEffectDeath, transform.position, transform.rotation);

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer spr in sprites)
        {
            spr.enabled = false;
        }

        Rigidbody2D[] rbs = GetComponentsInChildren<Rigidbody2D>();

        foreach(Rigidbody2D rb in rbs)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
        rb.isKinematic = true;
        GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<GameManager>().StartCoroutine("Restart");

        isDead = true;
    }

    IEnumerator Flash()
    {

        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.08f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void SetHealth(float health)
    {
        
        currentHealth = health;
        FindObjectOfType<HealthBar>().SetHealth(currentHealth);
    }

}
