using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    public bool pausePanelIsOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanelIsOpen)
            {
                pausePanel.SetActive(true);
                Pause();
                return;
            }
            if (pausePanelIsOpen)
            {
                pausePanel.SetActive(false);
                Continue();
                return;
            }
            //pausePanelIsOpen = !pausePanelIsOpen;
        }

    }

    public void Pause ()
    {
        Time.timeScale = 0;
        pausePanelIsOpen = true;        
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pausePanelIsOpen = false;        
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


}
