using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemtRandomShooting : MonoBehaviour
{

    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimeBtwShots <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
