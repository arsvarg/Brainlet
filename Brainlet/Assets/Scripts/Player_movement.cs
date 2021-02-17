﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    [SerializeField] float _force;
    [SerializeField] float _maxSpeed;
    
    public float directionOffset = 0f;



    Rigidbody2D rb;
    public Camera cam;


    Vector2 mousePos;
    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }

    void FixedUpdate()
    {

        

        rb.AddForce(movement * _force * Time.fixedDeltaTime);

        if (rb.velocity.magnitude > _maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * _maxSpeed;
        }


        //rb.MovePosition(rb.position + movement * _moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f + directionOffset;
        rb.rotation = angle;
    }
}
