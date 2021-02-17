using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 25f;
    float delay = .5f;
    float nextAttack;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && nextAttack <= Time.time)
        {
            collision.gameObject.GetComponent<Player_health>().TakeDamage(damage);
            nextAttack = Time.time + delay;

        }
    }
   
}
