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
    GameObject walkPoint;
    Vector3 walkPointSetter;
    public LayerMask whatIsSolid;
    bool walkPointSet;
    [SerializeField] float timeBetweenChangeDirection = 3f;
    
    
    
    float previousDirectionChangeTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        walkPoint = Instantiate(walkPointPrefab, transform.position, transform.rotation);
        GetComponent<AIDestinationSetter>().target = walkPoint.transform;
    }

    void Update()
    {
        

        if (Time.time >= previousDirectionChangeTime + timeBetweenChangeDirection)
        {
            ChangeDirection();

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

}
