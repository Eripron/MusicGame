using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] bool isUp;

    void Update()
    {
        Vector3 dir = isUp ? Vector3.up : Vector3.down;

        transform.RotateAround(target.transform.position, dir, 180 * Time.deltaTime);     
    }

}
