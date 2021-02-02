using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{
    public GameObject GameOverPanel;
    public AudioSource Click;


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        Click.Play();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Click.Play();
    }
        
}
