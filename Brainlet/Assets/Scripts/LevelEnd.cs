using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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



        if (collision.gameObject.tag == "Player" && canGoToNextLevel)
        {
            if (SceneManager.GetActiveScene().name == "level3")
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                FindObjectOfType<GameManager>().StartCoroutine("LoadNextLevel");
            }
            
        } 
    }
}
