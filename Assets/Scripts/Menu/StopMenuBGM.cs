using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenuBGM : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectOfType<MainMenuBGM>())
        {
            FindObjectOfType<MainMenuBGM>().StopBGM();
        }
    }
}