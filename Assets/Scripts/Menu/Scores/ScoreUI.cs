using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUi;
    public ScoreController scoreController;

    void Start()
    {

        SetUpUI();
    }

    public void SetUpUI()
    {
        var scores = scoreController.GetHighScores().ToArray();

        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.round.text = scores[i].round.ToString();
            row.score.text = scores[i].score.ToString();
        }
    }

    public void ResetUI()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ResetScore()
    {
        scoreController.DeleteData();
        ResetUI();
        SetUpUI();
    }
}
