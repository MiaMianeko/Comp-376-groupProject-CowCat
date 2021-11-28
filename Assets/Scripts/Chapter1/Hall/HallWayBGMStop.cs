using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallWayBGMStop : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectOfType<HallWayBGM>())
        {
            FindObjectOfType<HallWayBGM>().StopBGM();
        }
    }
}