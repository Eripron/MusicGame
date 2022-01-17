using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlate : MonoBehaviour
{
    [SerializeField] bool flag = true;

    public bool Flag { get { return flag; } private set { } }

    public void FlagDown()
    {
        flag = false;
    }



}
