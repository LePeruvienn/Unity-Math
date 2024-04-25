using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    private AudioManager AudioManager;

    public GameObject ScreenGameOver;

    public TextMeshProUGUI TextMeshPro;

    public void Setup(int score)
    {
        Debug.Log("Gameover 2");
        this.AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        this.AudioManager.PlayGameOver();
        this.AudioManager.StopBackGroundMusic();
        ScreenGameOver.SetActive(true);
        TextMeshPro.text = "Score : " + score.ToString();

    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
