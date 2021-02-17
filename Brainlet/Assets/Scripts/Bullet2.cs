using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    [SerializeField] float bulletForce;
    [SerializeField] float fireRate;
    [SerializeField] float damage;
    [SerializeField] float lifetime = .5f;
    [SerializeField] float pushForce;

    float timeToDie;

    Rigidbody2D rb;


    void Start()
    {
        FindObjectOfType<Player_shooting>().fireRate = fireRate;
        
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
        
        timeToDie = Time.time + lifetime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Box>())
        {
            collision.gameObject.GetComponent<Box>().TakeDamage(damage);
        }
        else if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(rb.velocity.normalized * pushForce);
        }
        Destroy(gameObject);
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, FindObjectOfType<Player_movement>().transform.position) > 50f)
        {
            Destroy(gameObject);
        }

        if (Time.time >= timeToDie)
        {
            Destroy(gameObject);
        }
    }
}
