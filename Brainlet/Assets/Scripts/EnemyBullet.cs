using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float lifetime = 2f;
    [SerializeField] float bulletForce;

    public float speed;
    public Vector2 targetN;
    float timeToDie;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(targetN * bulletForce, ForceMode2D.Impulse);
        

        timeToDie = Time.time + lifetime;
    }
    private void Update()
    {
        if (Time.time >= timeToDie)
        {
            Destroy(gameObject);
        }
    }

    public void send(Vector3 vector) {
        rb.AddForce(vector * bulletForce, ForceMode2D.Impulse);
    }
}
