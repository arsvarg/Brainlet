using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float spawnHealthGlobal;

    [SerializeField] float restartDelay = 1.5f;
    [SerializeField] int lastLevel = 4;

    void Awake()
    {
        spawnHealthGlobal = FindObjectOfType<Player_health>().maxHealth;
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
    }

    public IEnumerator Restart()
    {
        
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<Player_health>().SetHealth(FindObjectOfType<Player_health>().maxHealth);
    }

    public void StartGame()
    {
        StartCoroutine(LoadNextLevel());
    }

    public IEnumerator LoadNextLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        
        
        //Тут мог бы быть ваш красивый переход между сценами
        yield return new WaitForSeconds(0.5f);

        if (scene == lastLevel)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


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
        GameObject.Find("Player").GetComponent<Player_health>().SetHealth(spawnHealthGlobal);



    }

}
