using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallWayBGM : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;

    void Start()
    {
        if (FindObjectsOfType<HallWayBGM>().Length == 1)
        {
            bgmSource.Play();
            DontDestroyOnLoad(bgmSource);
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
}