using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{

    public GameObject options;
    public GameObject pauseMenu;
    public bool isPaused;
    
    void Start()
    {
        pauseMenu.SetActive(false);
        options.SetActive(false);
        this.isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        this.isPaused = true;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time .timeScale = 1;
        this.isPaused = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        this.isPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void ShowOptions()
    {
        pauseMenu.SetActive(false);
        options.SetActive(true);
    }
    
    public void ShowPause()
    {
        pauseMenu.SetActive(true);
        options.SetActive(false);
    }
}
