using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] float noteSpeed;

    UnityEngine.UI.Image noteImage;


    void OnEnable()
    {
        if(noteImage == null)
            noteImage = GetComponent<UnityEngine.UI.Image>();

        noteImage.enabled = true;
    }


    public void HideNote()
    {
        noteImage.enabled = false;
    }

    void Update()
    {
        transform.localPosition += noteSpeed * Time.deltaTime * Vector3.right;   
    }


    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
