using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] Transform[] plates;

    public Transform[] GetPlatesTf()
    {
        return plates;
    }

}
