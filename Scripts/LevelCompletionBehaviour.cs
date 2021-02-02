
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelCompletionBehaviour : MonoBehaviour
{
    public AudioSource Click;
    public GameObject LevelCompletionPanel;


    public void NextLevel()
    {
    Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex + 1 != null)
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
        Time.timeScale = 1;
        LevelCompletionPanel.SetActive(false);
        Click.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        LevelCompletionPanel.SetActive(false);
        Click.Play();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Click.Play();
    }
}
