using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyIt : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}