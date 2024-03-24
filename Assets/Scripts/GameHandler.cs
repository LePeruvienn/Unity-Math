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

    // Game Objects
    public List<GameObject> listeZombieSpawner;

    private Stats stats;

    private void Start()
    {
        this.score = 0;
        this.numManche = 1;
        this.zombieEnVie = 0;
        
        this.stats = GameObject.FindGameObjectWithTag("Score").GetComponent<Stats>();
    }


    // Méthodes secondaires

    public void addScore(int score)
    {
        this.score += score;
    }

    public void addManche()
    {
        this.numManche++;
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
