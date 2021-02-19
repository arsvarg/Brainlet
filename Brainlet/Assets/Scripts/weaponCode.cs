using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCode : MonoBehaviour
{
    [SerializeField] float NoPickUpTime = 15f;
    public int _weaponCode;
    SpriteRenderer spr;
    public Color disabledColor;

     void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        
    }

    public void StartWaiting()
    {
        StartCoroutine(WaitForPickUp());
    }

    IEnumerator WaitForPickUp()
    {

        GetComponent<PolygonCollider2D>().enabled = false;
        float currTime = 0f;

        while(currTime <= NoPickUpTime)
        {
            
            spr.color = Color.Lerp(disabledColor, Color.white, (currTime / NoPickUpTime));
            currTime += Time.deltaTime;
            yield return null;
        }


        spr.color = Color.white;
        GetComponent<Animator>().SetTrigger("popup");

        //yield return new WaitForSeconds(NoPickUpTime);
        //GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

}
