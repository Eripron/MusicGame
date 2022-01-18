using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;

    [SerializeField] TMP_Text[] textCount = null;
    [SerializeField] TMP_Text textCombo = null;
    [SerializeField] TMP_Text textScore = null;
    [SerializeField] TMP_Text textCoin = null;

    ScoreManager theScore;
    TimingManager theTiming;

    const string ZERO = "0";

    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theTiming = FindObjectOfType<TimingManager>();
    }
    

    public void ShowResult()
    {
        goUI.SetActive(true);

        for (int i = 0; i < textCount.Length; i++)
            textCount[i].text = ZERO;

        textCombo.text = ZERO;
        textScore.text = ZERO;
        textCoin.text = ZERO;

        int[] t_juegement = theTiming.GetJudgementRecord();
        int t_currentScore = theScore.GetCurrentScore();
        int t_maxCombo = theScore.GetMaxCombo();

        for (int i = 0; i < t_juegement.Length; i++)
            textCount[i].text = t_juegement[i].ToString();

        textScore.text = string.Format("{0:#,##0}", t_currentScore.ToString());
        textCombo.text = string.Format("{0:#,##0}", t_maxCombo.ToString());

        // 코인 구현 해야한다.
    }
}
