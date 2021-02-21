using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] float restartDelay = 1.5f;

    void Start()
    {

        InvokeRepeating("Scan", 5f, 2f);
    }

    void Update()
    {
        
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EnableGame()
    {
        StartCoroutine(EnableGame_c());
    }

    IEnumerator EnableGame_c()
    {

        FindObjectOfType<AudioManager>().Play("music");

        yield return new WaitForSeconds(0.7f);
        GameObject.Find("Player").GetComponent<Player_movement>().enabled = true;
        GameObject.Find("Player").GetComponent<Player_shooting>().enabled = true;
        GameObject.Find("Player").GetComponent<CircleCollider2D>().enabled = true;
        

    }

    void Scan()
    {
        FindObjectOfType<AstarPath>().Scan();
    }
}
