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


        if (collision.collider.gameObject.tag == "weapon" && nextAttack <= Time.time)
        {
            Debug.Log("Враг потрогал " + collision.gameObject);

            collision.gameObject.GetComponentInChildren<Weapon_script>().TakeDamage(damage);
            nextAttack = Time.time + delay;

        }


        if (collision.collider.gameObject.tag == "Player" && nextAttack <= Time.time)
        {
            collision.gameObject.GetComponentInChildren<Player_health>().TakeDamage(damage);
            nextAttack = Time.time + delay;

        }


        
    }
   
}
