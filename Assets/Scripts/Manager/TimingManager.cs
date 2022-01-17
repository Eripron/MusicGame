using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;



    // perfect, cool, good, bad
    Vector2[] timingBoxs = null;

    EffectManager theEffect;
    ScoreManager theScore;


    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScore = FindObjectOfType<ScoreManager>();

        timingBoxs = new Vector2[timingRect.Length];
        
        for(int i=0; i<timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }


    public void AddNoteToList(GameObject note)
    {
        boxNoteList.Add(note);
    }
    public void RemoveNoteToList(GameObject note)
    {
        boxNoteList.Remove(note);
    }


    public bool CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;
            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if (t_notePosX >= timingBoxs[x].x && t_notePosX <= timingBoxs[x].y)
                {
                    // 이펙트 연출 
                    theEffect.JudgementEffect(x);
                    if(x < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();

                    // 노트 제거
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);

                    // 점수 증가
                    theScore.IncreaseScore(x);
                    return true;
                }
            }
        }

        theEffect.JudgementEffect(timingBoxs.Length);
        theScore.ResetCombo();
        return false;
    }


}
