using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    bool canGoToNextLevel = false;
    bool changingLevel = false;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            canGoToNextLevel = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Сохранение информации об игроке
        


        if (collision.gameObject.tag == "Player" && canGoToNextLevel && !changingLevel)
        {
                FindObjectOfType<GameManager>().StartCoroutine("LoadNextLevel");
                changingLevel = true;
        } 
    }
}
