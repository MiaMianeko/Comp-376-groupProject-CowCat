using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBGM : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;

    void Start()
    {
        if (FindObjectsOfType<MainMenuBGM>().Length == 1)
        {
            bgmSource.Play();
            DontDestroyOnLoad(bgmSource);
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
        Destroy(bgmSource);
        Destroy(this);
    }
}