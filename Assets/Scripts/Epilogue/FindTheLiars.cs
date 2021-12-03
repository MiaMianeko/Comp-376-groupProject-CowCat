using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTheLiars : MonoBehaviour
{
    [SerializeField] GameObject puzzlePanel;
    bool q1Answered;
    bool q2Answered;
    bool q3Answered;

    [SerializeField] GameObject q1LieText;
    [SerializeField] GameObject q2LieText;
    [SerializeField] GameObject q3LieText;

    [SerializeField] GameObject q1TruthButton;
    [SerializeField] GameObject q2TruthButton;
    [SerializeField] GameObject q3TruthButton;

    [SerializeField] GameObject q1LieButton;
    [SerializeField] GameObject q2LieButton;
    [SerializeField] GameObject q3LieButton;

    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip wrong;
    [SerializeField] AudioClip right;

    float solvedTime;
    bool solved;

    RevelationsManager manager;

    void Start()
    {
        manager = FindObjectOfType<RevelationsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!solved && q1Answered && q2Answered && q3Answered)
        {
            solvedTime = Time.time;
            solved = true;
        }
        if(solved && Time.time> solvedTime + 2)
        {
            manager.incrementSteps();
        }
    }

    public void truthButtonClick(int i)
    {
        switch (i)
        {
            case 1:
                q1TruthButton.SetActive(false);
                audio.PlayOneShot(wrong);
                break;
            case 2:
                q2TruthButton.SetActive(false);
                audio.PlayOneShot(wrong);
                break;
            case 3:
                q3TruthButton.SetActive(false);
                audio.PlayOneShot(wrong);
                break;
        }
    }
    public void lieButtonClick(int i)
    {
        switch (i)
        {
            case 1:
                q1TruthButton.SetActive(false);
                q1LieButton.SetActive(false);
                audio.PlayOneShot(right);
                q1LieText.SetActive(true);
                q1Answered = true;
                break;
            case 2:
                q2TruthButton.SetActive(false);
                q2LieButton.SetActive(false);
                audio.PlayOneShot(right);
                q2LieText.SetActive(true);
                q2Answered = true;
                break;
            case 3:
                q3TruthButton.SetActive(false);
                q3LieButton.SetActive(false);
                audio.PlayOneShot(right);
                q3LieText.SetActive(true);
                q3Answered = true;
                break;
        }

    }

}
