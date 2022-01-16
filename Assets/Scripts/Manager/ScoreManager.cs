using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text txtScore = null;

    [SerializeField] int increaseScore = 10;
    int currentScore = 0;

    [SerializeField] float[] weight = null;

    Animator scoreAnim;
    const string SCORE_UP = "ScoreUp";

    void Start()
    {
        currentScore = 0;
        txtScore.text = currentScore.ToString();

        scoreAnim = GetComponent<Animator>();
    }


    public void IncreaseScore(int p_JudgementState)
    {
        // 점수 증가 
        int t_increaseScore = increaseScore;
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);

        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        // 점수 animator 
        scoreAnim.SetTrigger(SCORE_UP);
    }
}
