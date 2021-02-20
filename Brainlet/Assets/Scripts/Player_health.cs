using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    Rigidbody2D rb;
    [SerializeField] GameObject particleEffectDeath;

    public HealthBar healthBar;

    bool isDead;

    public float p_currentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Flash());
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, maxHealth);
        healthBar.SetHealth(currentHealth);

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

}
