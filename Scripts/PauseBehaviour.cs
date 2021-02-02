using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour
{
    public AudioSource Click;
    //public AudioListener MGCam;

    public GameObject Pausepanel;
    public GameObject SoundOn;
    public GameObject SoundOff;




    private void Start()
    {
        //MGCam = GetComponent<AudioListener>();
    }
    public void Resume()
    {
        Pausepanel.SetActive(false);
        Time.timeScale = 1;
        Click.Play();
    }
    public void Restart()
    {
        Pausepanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Click.Play();
    }
    public void MenuMain()
    {
        SceneManager.LoadScene("MainMenu");
        Click.Play();
    }
    public void SoundPlay()
    {
        if (SoundOn.activeInHierarchy)
        {
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
            Click.Play();
            AudioListener.pause = true;
        }
        else if (SoundOff.activeInHierarchy)
        {
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
            Click.Play();
            AudioListener.pause = false;
        }
    }
}
