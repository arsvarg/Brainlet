using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    public float moveSpeed;
    private bool moving;
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;
    private Vector3 moveDirection;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject bullet;

    private Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;

        timeBtwShots = startTimeBtwShots;
    }

    void FixedUpdate()
    {
        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            myRigidBody.velocity = moveDirection;

            if (timeToMoveCounter < 0f) {
                moving = false;
                timeBetweenMoveCounter = timeBetweenMove;
            }
        }
        else {
            timeBetweenMoveCounter -= Time.deltaTime;
            myRigidBody.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0f) {
                moving = true;
                timeToMoveCounter = timeToMove;
                moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f) * moveSpeed;
            }
        }

        if (timeBtwShots <= 0)
        {
            GameObject n = Instantiate(bullet, transform.position, Quaternion.identity);
            n.GetComponent<EnemyBullet>().send(transform.up);
            GameObject s = Instantiate(bullet, transform.position, Quaternion.identity);
            s.GetComponent<EnemyBullet>().send(-transform.up);
            GameObject w = Instantiate(bullet, transform.position, Quaternion.identity);
            w.GetComponent<EnemyBullet>().send(-transform.right);
            GameObject e = Instantiate(bullet, transform.position, Quaternion.identity);
            e.GetComponent<EnemyBullet>().send(transform.right);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Не работает какого то хуя
        moveDirection = -myRigidBody.velocity;
    }
}
