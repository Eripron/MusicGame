using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject target;

    public float angle;
    public float radius = 10;
    public float degreesPerSecond = 30;

    private void Update()
    {
        angle += degreesPerSecond * Time.deltaTime;

        if(angle > 360)
        {
            angle -= 360;
        }

        Vector3 orbit = Vector3.forward * radius;
        orbit = Quaternion.Euler(0, angle, 0) * orbit;

        transform.position = target.transform.position + orbit;
    }


}
