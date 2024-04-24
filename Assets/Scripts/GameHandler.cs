using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    //Statisques

    private int score;
    private int numManche;
    private int zombieEnVie;

    //Stats
    private Stats stats;

    private void Start()
    {
        this.score = 0;
        this.numManche = 0;
        this.zombieEnVie = 0;
        Application.targetFrameRate = 120;
        this.stats = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Stats>();
    }


    // Méthodes secondaires

    public void addScore(int score)
    {
        this.score += score;
    }

    public void addManche()
    {
        this.numManche++;
        stats.playNewManche();
    }

    //Getters

    public int getScore()
    {
        return score;
    }

    public int getNumManche()
    {
        return numManche;
    }

    public int getZombieEnVie()
    {
        return zombieEnVie;
    }
}
