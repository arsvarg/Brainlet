﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float bulletForce;
    [SerializeField] float damage;
    [SerializeField] float lifetime = .5f;
    [SerializeField] float pushForce;
    [SerializeField] float recoilForce;

    float timeToDie;

    Rigidbody2D rb;

    public float speed;

    private Transform player;
    private Vector2 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
        timeToDie = Time.time + lifetime;
        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y) {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "weapon")
        {
            
            collision.collider.gameObject.GetComponent<Weapon_script>().TakeDamage(damage);
        }
        
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_health>().TakeDamage(damage);
            
        }
        
        if (collision.gameObject.GetComponent<Box>())
        {
            collision.gameObject.GetComponent<Box>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    
}
