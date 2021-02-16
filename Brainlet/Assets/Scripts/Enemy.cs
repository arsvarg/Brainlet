using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float pathfinderAreaRadius = 10;
    float currenHealth;


    void Start()
    {
        currenHealth = maxHealth;

    }
    public void TakeDamage(float damage) {
        currenHealth -= damage;
        if (currenHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, FindObjectOfType<Player_movement>().transform.position) <= pathfinderAreaRadius)
        {
            
            GetComponent<Pathfinding.AIDestinationSetter>().target = FindObjectOfType<Player_movement>().transform;
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            
        }
        else
        {
            GetComponent<Pathfinding.AIDestinationSetter>().target = null;
        }
    }
}
