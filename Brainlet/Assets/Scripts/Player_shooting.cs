using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_shooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletForce;
    [SerializeField] Transform[] firePoints;
    [SerializeField] GameObject[] Weapons;
    [SerializeField] GameObject[] bulletPrefabs;
    [SerializeField] float laserLength = 50f;
    [SerializeField] LayerMask stopLaser;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float laserDamage;
    [SerializeField] float laserFireRate = 0.5f;
    LineRenderer lineRenderer;


    public GameObject bulletPrefab;
    Player_movement player_movement;
    public float fireRate = 0f;
    float nextShootTime;
    float previousOffset;
    int previousWeapon;
    public AudioSource sound;
    Animator animator;

    CinemachineImpulseSource ImpulseSource;


    int chosenWeapon = 1;

    void Start()
    {
        player_movement = FindObjectOfType<Player_movement>();
        ImpulseSource = GetComponent<CinemachineImpulseSource>();
        animator = GetComponent<Animator>();
        lineRenderer = GetComponentInChildren<LineRenderer>();

        ChangingWeapon(chosenWeapon);
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
                    Debug.Log(fireRate);
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
            ChangingWeapon(1);
        }

        if (Input.GetKeyDown("2"))
        {
            ChangingWeapon(2);
        }
        if (Input.GetKeyDown("3"))
        {
            ChangingWeapon(3);
        }
       
        if (Input.GetButtonDown("Shield"))
        {
            previousOffset = player_movement.directionOffset;
            previousWeapon = chosenWeapon;
        }
        if (Input.GetButton("Shield"))
        {
            player_movement.directionOffset = -180f;
            chosenWeapon = 4;

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
            FindObjectOfType<AudioManager>().Play("shot");
            animator.SetTrigger("shoot");
            

            ImpulseSource.GenerateImpulse(bullet.transform.up.normalized * 4f);
        }

        if (chosenWeapon == 2)
        {
            fireRate = laserFireRate;

            RaycastHit2D wallHit = Physics2D.Raycast(firePoints[1].position, firePoints[1].right, laserLength, stopLaser);

            float rayLength;
            Vector2 endPosition;
            Vector2 startPosition = firePoints[1].position;

            if (wallHit)
            {
                
                lineRenderer.SetPosition(0, firePoints[1].position);
                lineRenderer.SetPosition(1, wallHit.point);
                rayLength = wallHit.distance;
                endPosition = wallHit.point;


            }
            else
            {
                rayLength = laserLength;
                Debug.Log(wallHit.distance);
                lineRenderer.SetPosition(0, firePoints[1].position);
                lineRenderer.SetPosition(1, firePoints[1].position + firePoints[1].right * 100f);
                endPosition = firePoints[1].position + firePoints[1].right * 100f;
            }

            



            RaycastHit2D[] hits = Physics2D.RaycastAll(firePoints[1].position, firePoints[1].right, rayLength, enemyLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.transform.gameObject.tag == "enemy")
                {
                    
                    hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(laserDamage);
                    
                }
                if (hit.transform.gameObject.GetComponent<Box>())
                {
                    hit.transform.gameObject.GetComponent<Box>().TakeDamage(laserDamage);
                }

                
                
            }

            ImpulseSource.GenerateImpulse(startPosition.normalized * 10f);
            StartCoroutine(LaserShow(startPosition, endPosition));

        }
        if (chosenWeapon == 3)
        {
            GameObject bullet = Instantiate(bulletPrefabs[2], firePoints[2].position, firePoints[2].rotation);
            
        }
       
        if (chosenWeapon == 4)
        {
            sound.Play();
        }


    }

    IEnumerator LaserShow(Vector2 startPosition, Vector2 endPosition)
    {
        lineRenderer.enabled = true;



        // Lerp for ray
        // float laserShowTime = 0.6f;
        // float currTime = 0f;
        //
        //while (currTime <= laserShowTime)
        //{
        //    lineRenderer.SetPosition(1, Vector2.Lerp(startPosition, endPosition, (currTime / laserShowTime)));
        //    currTime += Time.deltaTime;
        //    yield return null;
        //}
        //lineRenderer.SetPosition(1, endPosition);
        yield return new WaitForSeconds(0.3f);


        lineRenderer.enabled = false;
    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<weaponCode>())
        {
            Weapons[collision.GetComponent<weaponCode>()._weaponCode - 1].SetActive(true);
            Weapons[collision.GetComponent<weaponCode>()._weaponCode - 1].GetComponent<Weapon_script>().RestoreHP();
            Destroy(collision.gameObject);
        }
    }


    void ChangingWeapon(int _chosenWeapon)
    {
        fireRate = Weapons[_chosenWeapon - 1].GetComponent<Weapon_script>().fireRate;
        nextShootTime = Time.time + 0.1f;

        if (_chosenWeapon == 1)
        {
            player_movement.directionOffset = 0f;
            chosenWeapon = 1;
            
        }

        if (_chosenWeapon == 2)
        {
            player_movement.directionOffset = 90f;
            chosenWeapon = 2;
            
        }
        if (_chosenWeapon == 3)
        {
            player_movement.directionOffset = -90f;
            chosenWeapon = 3;
            
        }


    }
}
