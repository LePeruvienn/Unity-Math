using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public GameObject ScreenGameOver;

    public TextMeshProUGUI TextMeshPro;
    public void Setup(int score)
    {
        Debug.Log("DEAD SCREEN");
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
