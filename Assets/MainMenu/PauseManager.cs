using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public AudioSource bgMusic;
    private bool bgMusicOn = false;

    public void PauseGame()
    {
        if (bgMusic.isPlaying)
        {
            bgMusicOn = true;
            bgMusic.Pause();
        }
        else
            bgMusicOn = false; 
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ResumeButton()
    {
        if (bgMusicOn)
            bgMusic.UnPause();
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


}
