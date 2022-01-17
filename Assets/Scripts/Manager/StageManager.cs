using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] Stage stage = null;

    Transform[] stagePlates;

    [SerializeField] float offsetY = 3.0f;
    [SerializeField] float plateSpeed = 10.0f;


    int stepCount = 0;
    int totalPlateCount = 0;

    void Start()
    {
        stagePlates = stage.GetPlatesTf();
        totalPlateCount = stagePlates.Length;

        for(int i=0; i<totalPlateCount; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x, 
                                                  stagePlates[i].position.y - offsetY, 
                                                  stagePlates[i].position.z);
        }
    }

    public void ShowNextPlate()
    {
        if(stepCount < totalPlateCount)
        {
            StartCoroutine(MovePlateCo(stepCount++));
        }
    }

    IEnumerator MovePlateCo(int index)
    {
        stagePlates[index].gameObject.SetActive(true);

        Vector3 destPos = new Vector3(stagePlates[index].position.x,
                                      stagePlates[index].position.y + offsetY,
                                      stagePlates[index].position.z);

        while(Vector3.SqrMagnitude(stagePlates[index].position - destPos) >= 0.001f)
        {
            stagePlates[index].position = Vector3.Lerp(stagePlates[index].position, destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }

        stagePlates[index].position = destPos;
    }

}
