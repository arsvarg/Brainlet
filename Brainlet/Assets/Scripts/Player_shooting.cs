using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletForce;
    [SerializeField] Transform[] firePoints;
    [SerializeField] GameObject[] Weapons;
    [SerializeField] GameObject[] bulletPrefabs;

    public GameObject bulletPrefab;
    Player_movement player_movement;
    public float fireRate = 2f;
    float nextShootTime;
    float previousOffset;
    int previousWeapon;
    public AudioSource sound;
    

    int chosenWeapon = 1;

    void Start()
    {
        player_movement = FindObjectOfType<Player_movement>();
        
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            if (Input.GetButton("Fire1"))
            {
                if (Weapons[chosenWeapon - 1].activeSelf)
                {
                    Shoot(chosenWeapon);
                    nextShootTime = Time.time + 1f / fireRate;
                }
                else
                {
                    Debug.Log("No weapon here!");
                }

            }

        }



        if (Input.GetKeyDown("1"))
        {
            player_movement.directionOffset = 0f;
            chosenWeapon = 1;
        }

        if (Input.GetKeyDown("2"))
        {
            player_movement.directionOffset = 60f;
            chosenWeapon = 2;
        }
        if (Input.GetKeyDown("3"))
        {
            player_movement.directionOffset = 120f;
            chosenWeapon = 3;
        }
        if (Input.GetKeyDown("4"))
        {
            player_movement.directionOffset = 180f;
            chosenWeapon = 4;
        }
        if (Input.GetKeyDown("6"))
        {
            player_movement.directionOffset = -60f;
            chosenWeapon = 6;
        }
        if (Input.GetButtonDown("Shield"))
        {
            previousOffset = player_movement.directionOffset;
            previousWeapon = chosenWeapon;
        }
        if (Input.GetButton("Shield"))
        {
            player_movement.directionOffset = -120f;
            chosenWeapon = 5;
        }
        if (Input.GetButtonUp("Shield"))
        {
            player_movement.directionOffset = previousOffset;
            chosenWeapon = previousWeapon;
        }
    }

    void Shoot(int chosenWeapon)
    {
        if (chosenWeapon == 1)
        {
            GameObject bullet = Instantiate(bulletPrefabs[0], firePoints[0].position, firePoints[0].rotation);
            
        }

        if (chosenWeapon == 2)
        {
            
        }
        if (chosenWeapon == 3)
        {
            GameObject bullet = Instantiate(bulletPrefabs[2], firePoints[2].position, firePoints[2].rotation);
            
        }
        if (chosenWeapon == 4)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoints[3].position, firePoints[3].rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(firePoints[3].up * bulletForce, ForceMode2D.Impulse);
        }
        if (chosenWeapon == 5)
        {
            sound.Play();
        }
        if (chosenWeapon == 6)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoints[5].position, firePoints[5].rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(firePoints[5].up * bulletForce, ForceMode2D.Impulse);
        }




    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<weaponCode>())
        {
            Weapons[collision.GetComponent<weaponCode>()._weaponCode - 1].SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}
