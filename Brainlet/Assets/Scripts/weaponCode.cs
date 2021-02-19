using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCode : MonoBehaviour
{
    float NoPickUpTime = 15f;
    public int _weaponCode;
    public Color disableColor;
    SpriteRenderer spr;

    


    public void StartWaiting()
    {
        spr = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitForPickUp());
        
    }

     IEnumerator WaitForPickUp()
    {

        GetComponent<PolygonCollider2D>().enabled = false;
        float currTime = 0f;
        GetComponent<Animation>().Play();

        do {
            //spr.color = Color.Lerp(spr.color, Color.white, currTime / NoPickUpTime);
            //if (spr.color == Color.white)
            //{
            //    Debug.Log("Я белый!!! " + currTime);
            //}
            currTime += Time.deltaTime;
            yield return null;
        } while (currTime <= NoPickUpTime);        
        


        GetComponent<PolygonCollider2D>().enabled = true;
    }



   

}
