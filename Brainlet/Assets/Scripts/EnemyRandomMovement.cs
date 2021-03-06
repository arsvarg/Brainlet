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
    public LayerMask whatIsSolid;
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
                    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

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

    IEnumerator RotateAndFire(float angle, float rotationDuration)
    {
        float startRotation = rb.rotation;
        float endRotation = angle;
        float currTime = 0f;

        while (currTime <= rotationDuration)
        {
            rb.rotation = Mathf.Lerp(startRotation, endRotation, (currTime / rotationDuration));
            currTime += Time.deltaTime;
            yield return null;

        }

        rb.rotation = endRotation;
        Instantiate(enemyBullet, transform.Find("FirePoint").gameObject.transform.position, Quaternion.identity);


    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, shootingRadius);
    }

}
