using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    bool canGoToNextLevel = false;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            canGoToNextLevel = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canGoToNextLevel)
        {
            FindObjectOfType<GameManager>().StartCoroutine("LoadNextLevel");
        } 
    }
}
