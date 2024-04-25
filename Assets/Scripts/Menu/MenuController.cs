using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject score;
    public GameObject options;
    public GameObject credit;
    public GameObject tuto;


    void Start()
    {
        BackToMain();
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        this.menu.SetActive(true);
        this.score.SetActive(false);
        this.options.SetActive(false);
        this.credit.SetActive(false);
    }

    // GO TO
    public void GoToScore()
    {
        this.menu.SetActive(false);
        this.score.SetActive(true);
        this.options.SetActive(false);
        this.credit.SetActive(false);
    }

    public void GoToCredit() 
    {
        this.menu.SetActive(false);
        this.score.SetActive(false);
        this.options.SetActive(false);
        this.credit.SetActive(true);
    }

    public void GoToOptions()
    {
        this.menu.SetActive(false);
        this.score.SetActive(false);
        this.options.SetActive(true);
        this.credit.SetActive(false);
    }
}
