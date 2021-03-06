using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] float restartDelay = 1.5f;

    void Start()
    {

        //InvokeRepeating("Scan", 5f, 2f);
    }

    void Update()
    {
        
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        StartCoroutine(LoadNextLevel());
    }

    public IEnumerator LoadNextLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.GetActiveScene().name == "level3")
        {
            scene = -1;
        }

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

    public void Scan(Bounds bounds)
    {
        FindObjectOfType<AstarPath>().UpdateGraphs(bounds);
    }
}
