using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseToEndEvent : MonoBehaviour
{
    private bool isFirst = true;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isFirst)
        {
            FindObjectOfType<HallGameManager>().LoadDialog2();
            isFirst = false;
        }
 
    }
}