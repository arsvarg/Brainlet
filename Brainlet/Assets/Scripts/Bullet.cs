using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletForce;
    [SerializeField] float lifetime = .5f;
    [SerializeField] float pushForce;
    [SerializeField] float recoilForce;
    [SerializeField] GameObject effectPrefab;
    [SerializeField] float damage;
    
    float timeToDie;

    Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
        FindObjectOfType<Player_movement>().GetComponent<Rigidbody2D>().AddForce(-transform.up * recoilForce, ForceMode2D.Impulse);
        timeToDie = Time.time + lifetime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Box>())
        {
            collision.gameObject.GetComponent<Box>().TakeDamage(damage);
        }
        else if (collision.gameObject.GetComponent<Enemy>()) {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(rb.velocity.normalized * pushForce);
        }
        Instantiate(effectPrefab, transform.position, transform.rotation);
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
            Instantiate(effectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
