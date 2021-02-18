using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCode : MonoBehaviour
{
    static float NoPickUpTime = 15f;
    public int _weaponCode; 

    public IEnumerator WaitForPickUp()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(NoPickUpTime);
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

}
