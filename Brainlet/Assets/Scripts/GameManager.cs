using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text hp_text;
    [SerializeField] float restartDelay = 1.5f;

    void Start()
    {
        hp_text.text = "Health points: " + FindObjectOfType<Player_health>().maxHealth.ToString();

    }

    void Update()
    {
        hp_text.text = "Health points: " + FindObjectOfType<Player_health>().p_currentHealth.ToString();
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
