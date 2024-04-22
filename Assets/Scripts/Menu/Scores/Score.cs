using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    public string name;
    public int round;
    public int score;

    public Score(string name, int round, int score)
    {
        this.name = name;
        this.round = round;
        this.score = score;
    }
}
