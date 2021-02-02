using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject SoundOn;
    public GameObject SoundOff;

    AudioSource Click;

    private void Start()
    {
        Click = GetComponent<AudioSource>();
    }
    public void Play()
    {
        SceneManager.LoadScene("Level1");
        Click.Play();
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        Click.Play();
    }
    public void Quit()
    {
        Application.Quit();
        Click.Play();
    }
    public void SoundPlay()
    {
        if(SoundOn.activeInHierarchy)
        {
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
            Click.Play();
            AudioListener.pause = true;
        }
        else if(SoundOff.activeInHierarchy)
        {
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
            Click.Play();
            AudioListener.pause = false;
        }
    }
}
