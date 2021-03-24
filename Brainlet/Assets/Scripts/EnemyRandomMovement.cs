using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRandomMovement : MonoBehaviour
{
    public GameObject bullet;

    private Rigidbody2D rb;

    [SerializeField] float walkpointDistance = 1f;
    [SerializeField] GameObject walkPointPrefab;
    [HideInInspector] public GameObject walkPoint;
    Vector3 walkPointSetter;
    [SerializeField] LayerMask whatIsSolid;
    [SerializeField] LayerMask wall;
    [SerializeField] LayerMask canShoot;
    bool walkPointSet;
    [SerializeField] float timeBetweenChangeDirection = 3f;
    [SerializeField] float shootingRadius = 10f;
    [SerializeField] float rotationDuration = 0.2f;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject enemyBullet;
    bool playerNerby;
    float lookingRadius = 25;

    float previousDirectionChangeTime;
    public Transform firePoint;
    Quaternion angle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        walkPoint = Instantiate(walkPointPrefab, transform.position, transform.rotation);
        GetComponent<AIDestinationSetter>().target = walkPoint.transform;
        timeBtwShots = startTimeBtwShots;

    }

    void Update()
    {
        if (Vector2.Distance(transform.position, FindObjectOfType<Player_movement>().transform.position) <= lookingRadius)
        {
            playerNerby = true;
        }

        if (playerNerby)
        {
            if (Time.time >= previousDirectionChangeTime + timeBetweenChangeDirection)
            {
                ChangeDirection();

            }

            if (timeBtwShots <= 0)
            {
                if (Vector2.Distance(transform.position, FindObjectOfType<Player_movement>().transform.position) <= shootingRadius)
                {
                    Vector2 lookDir = (Vector2)FindObjectOfType<Player_movement>().transform.position - rb.position;
                    angle = Quaternion.Euler(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);

                    StartCoroutine(RotateAndFire(angle, rotationDuration));

                }

                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

        
    }

    void CalculateDirection()
    {
        Vector2 destinationVector;
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        destinationVector = new Vector2(randomX, randomY).normalized * walkpointDistance;



        walkPointSetter = new Vector3(transform.position.x + destinationVector.x, transform.position.y + destinationVector.y, transform.position.z);

        if (!Physics2D.OverlapPoint(walkPointSetter, whatIsSolid))
        {
            walkPointSet = true;
        }
    }

    void ChangeDirection()
    {
        walkPointSet = false;
        CalculateDirection();
        if (walkPointSet)
        {
            walkPoint.transform.position = walkPointSetter;
            previousDirectionChangeTime = Time.time;
            walkPointSet = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }

    IEnumerator RotateAndFire(Quaternion angle, float rotationDuration)
    {
        Quaternion startRotation = transform.rotation;
        
        float currTime = 0f;

        while (currTime <= rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, angle, (currTime / rotationDuration));
            currTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();

        }

        transform.rotation = angle;
        Fire();


    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, shootingRadius);
        
    }

    private void OnDrawGizmos()
    {

        RaycastHit2D checkWallGizmo = Physics2D.Raycast(firePoint.position, firePoint.right, shootingRadius, wall);
        if (checkWallGizmo)
        {
            Gizmos.DrawLine(firePoint.position, checkWallGizmo.point);
        }


    }




    void Fire()
    {

        //RaycastHit2D[] hits = Physics2D.BoxCastAll(new Vector2(firePoint.position.x, (firePoint.position.y + firePoint.position.y + shootingRadius) / 2), new Vector2(0.1f, checkWall.distance), angle.eulerAngles.z, firePoint.right);
        //foreach (RaycastHit2D item in hits)
        //{
        //    Debug.Log("I see " + item.collider.gameObject);
        //    if (item.collider.gameObject.tag == "Player")
        //    {

        //        playerIsOnFireLine = true;
        //    }
        //}
       
        //bool playerIsOnFireLine = false;

        RaycastHit2D checkWall = Physics2D.Raycast(firePoint.position, firePoint.right, shootingRadius, wall);

        float CheckWallDistance = shootingRadius;

        if (checkWall.distance != 0)
        {
            CheckWallDistance = checkWall.distance;
        }

        RaycastHit2D checkPlayer = Physics2D.Raycast(firePoint.position, firePoint.right, CheckWallDistance, 1);
        if (checkPlayer)
        {
            Debug.Log("checkPlayer hits " + checkPlayer.collider.gameObject);

            if (checkPlayer.collider.gameObject.tag == "Player")
            {
                Instantiate(enemyBullet, transform.Find("FirePoint").gameObject.transform.position, Quaternion.identity);
            }
        }
        else
            Debug.Log("checkPlayer hits nothing" );


        
        
    }

}
