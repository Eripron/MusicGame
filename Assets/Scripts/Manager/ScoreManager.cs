using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // �޺� 
    [Header("Combo")]
    [SerializeField] GameObject goComboImage = null;
    [SerializeField] TMP_Text txtCombo = null;
    int comboBounsScore = 10;

    int currentCombo = 0;
    int maxCombo = 0;

    // ���� 
    [Header("Score")]
    [SerializeField] TMP_Text txtScore = null;
    [SerializeField] int increaseScore = 10;
    [SerializeField] float[] weight = null;

    int currentScore = 0;


    Animator scoreAnim;
    const string SCORE_UP = "ScoreUp";
    const string COMBO_UP = "ComboUp";


    void Start()
    {
        currentScore = 0;
        txtScore.text = currentScore.ToString();

        scoreAnim = GetComponent<Animator>();

        ResetCombo();
    }


    public void IncreaseScore(int p_JudgementState)
    {
        // �޺����� 
        IncreaseCombo();
        // �޺� ���ʽ� ���� 
        int t_bonusComboScore = (currentCombo / 10) * comboBounsScore;

        // ���� ���� 
        int t_increaseScore = increaseScore + t_bonusComboScore;
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);

        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        // ���� animator 
        scoreAnim.SetTrigger(SCORE_UP);
    }

    public void IncreaseCombo(int p_num = 1)
    {
        currentCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);

        if (currentCombo > maxCombo)
            maxCombo = currentCombo;

        if(currentCombo > 2)
        {
            scoreAnim.SetTrigger(COMBO_UP);
            txtCombo.gameObject.SetActive(true);
            goComboImage.SetActive(true);
        }
    }
     
    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = currentCombo.ToString();
        txtCombo.gameObject.SetActive(false);
        goComboImage.SetActive(false);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
    public int GetMaxCombo()
    {
        return maxCombo;
    }
}
