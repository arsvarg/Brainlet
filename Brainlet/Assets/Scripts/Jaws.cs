using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaws : MonoBehaviour
{
    [SerializeField] float damage;
    float delay = .5f;
    float nextAttack;


    void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player" && nextAttack <= Time.time)
        {
            collision.gameObject.GetComponent<Player_health>().TakeDamage(damage);
            nextAttack = Time.time + delay;

        }


       
    }
    
    
}
