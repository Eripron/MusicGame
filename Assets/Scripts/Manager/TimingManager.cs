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

    // reference
    EffectManager theEffect;
    ScoreManager theScore;
    StageManager theStage;
    PlayerController thePlayer;

    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theStage = FindObjectOfType<StageManager>();
        thePlayer = FindObjectOfType<PlayerController>();

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
                    if(x < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();

                    // 노트 제거
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);
                   

                    if(CheckCanNextPlate())
                    {
                        theStage.ShowNextPlate();
                        theScore.IncreaseScore(x);  // 점수 증가
                        theEffect.JudgementEffect(x);
                    }
                    else
                    {
                        theEffect.JudgementEffect(5);
                    }

                    return true;
                }
            }
        }

        theEffect.JudgementEffect(timingBoxs.Length);
        theScore.ResetCombo();
        return false;
    }

    bool CheckCanNextPlate()
    {
        if(Physics.Raycast(thePlayer.Destination, Vector3.down, out RaycastHit hit, 2.0f))
        {
            if(hit.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_plate = hit.transform.GetComponent<BasicPlate>();
                if (t_plate.Flag)
                {
                    t_plate.FlagDown();
                    return true;
                }
            }
        }

        return false;
    }
}
