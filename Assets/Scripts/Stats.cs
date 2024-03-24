using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{

    private GameHandler gameHandler;

    private TextMeshProUGUI txtScore;
    private TextMeshProUGUI txtManche;

    void Start()
    {
        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        this.txtScore = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        this.txtManche = GameObject.FindGameObjectWithTag("Manche").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        txtScore.text = "Score : " + gameHandler.getScore();
        txtManche.text = "Manche : " + gameHandler.getNumManche();
    }
}
