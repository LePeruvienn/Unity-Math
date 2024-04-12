using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Stats : MonoBehaviour
{

    private GameHandler gameHandler;

    private TextMeshProUGUI txtScore;
    private TextMeshProUGUI txtManche;

    public GameObject newManche;

    void Start()
    {
        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        this.txtScore = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        this.txtManche = GameObject.FindGameObjectWithTag("Manche").GetComponent<TextMeshProUGUI>();
        newManche.SetActive(false);
    }

    void Update()
    {
        txtScore.text = "Score : " + gameHandler.getScore();
        txtManche.text = "Manche : " + gameHandler.getNumManche();
    }

    public void playNewManche()
    {
        StartCoroutine(animNewManche());
    }

    IEnumerator animNewManche()
    {
        this.newManche.GetComponent<TextMeshProUGUI>().text = "Manche " + gameHandler.getNumManche();
        this.newManche.SetActive(true);
        yield return new WaitForSeconds(5f);
        this.newManche.GetComponent<Animator>().SetTrigger("exit");
        yield return new WaitForSeconds(2f);
        this.newManche.SetActive(false);
    }


}
