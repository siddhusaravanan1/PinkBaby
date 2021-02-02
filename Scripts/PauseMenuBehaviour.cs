using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    public GameObject PausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausePanel.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                PausePanel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                PausePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
