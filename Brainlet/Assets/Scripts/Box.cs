using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Box : MonoBehaviour
{
    [SerializeField] float maxHP;
    float currentHP;
    
    
    void Start()
    {
        currentHP = maxHP;  
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            FindObjectOfType<AudioManager>().Play("box");
            FindObjectOfType<AstarPath>().UpdateGraphs(GetComponent<Collider2D>().bounds);
            Destroy(gameObject);
            
        }
    }
}
