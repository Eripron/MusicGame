using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour
{
    AudioSource myAudio;

    bool isMusicStart = false;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();     
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isMusicStart && collision.CompareTag("Note"))
        {
            isMusicStart = true;
            myAudio.Play();
        }
    }
}
