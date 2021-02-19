using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    
    float currenHealth;
    public Color hurtColor;
    [SerializeField] GameObject particleEffect;
    

    void Start()
    {
        currenHealth = maxHealth;

    }
    public void TakeDamage(float damage) {
        currenHealth -= damage;
        StartCoroutine(Flash());

        if (currenHealth <= 0) {
            Die();
        }
    }

    void Die() {

        Instantiate(particleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }

     IEnumerator Flash()
    {
        
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.08f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;

    }
}
