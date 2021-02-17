using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(gameObject);
        }
    }
}
