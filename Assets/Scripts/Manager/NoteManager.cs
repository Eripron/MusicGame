using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    // beat per min
    [SerializeField] int bpm;
    [SerializeField] Transform tfNoteAppear = null;

    double currentTime = 0d;

    bool noteActive = true;

    TimingManager theTimingManager;
    EffectManager theEffect;
    ScoreManager theScore;


    void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
        theEffect = FindObjectOfType<EffectManager>();
        theScore = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (!noteActive)
            return;

        currentTime += Time.deltaTime;     

        if(currentTime >= 60d / bpm)
        {
            GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
            t_note.transform.position = tfNoteAppear.position;
            t_note.SetActive(true);

            theTimingManager.AddNoteToList(t_note);
            currentTime -= 60d / bpm;
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Note"))
        {
            Note note = collision.GetComponent<Note>();
            if(note != null && note.GetNoteFlag())
            {
                theTimingManager.MissRecord();
                theEffect.JudgementEffect(4);
                theScore.ResetCombo();
            }

            theTimingManager.RemoveNoteToList(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }


    public void RemoveNote()
    {
        noteActive = false;
        theTimingManager.RemoveNote();
    }

}
