﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyMovement : MonoBehaviour
{
    [SerializeField] float pathfinderAreaRadius = 10;
    void Update()
    {
        if (Vector2.Distance(transform.position, FindObjectOfType<Player_movement>().transform.position) <= pathfinderAreaRadius)
        {

            

            GetComponent<Pathfinding.AIDestinationSetter>().target = FindObjectOfType<Player_movement>().transform;
            

        }
        else
        {
            GetComponent<Pathfinding.AIDestinationSetter>().target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
       
        Gizmos.DrawWireSphere(transform.position, pathfinderAreaRadius);
    }
}
