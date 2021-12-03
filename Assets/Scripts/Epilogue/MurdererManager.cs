using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurdererManager : MonoBehaviour
{

    float startingTime;
    bool started;
    [SerializeField] GameObject murdererText1;
    [SerializeField] GameObject murdererText2;
    [SerializeField] GameObject murdererText3;
    [SerializeField] GameObject murdererText4;
    [SerializeField] GameObject murdererText5;
    [SerializeField] GameObject murdererText6;
    [SerializeField] GameObject murdererText7;
    [SerializeField] GameObject murdererText8;
    [SerializeField] GameObject murdererText9;
    [SerializeField] GameObject murdererText10;
    float stage = 0;
    [SerializeField] AudioSource sound;

    RevelationsManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<RevelationsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
            switch (stage)
            {
                case 0:
                    murdererText1.SetActive(true);
                    sound.Play();
                    startingTime = Time.time;
                    stage++;
                    break;
                case 1:
                    if (Time.time > startingTime + 0.5f)
                    {
                        murdererText2.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;
                   
                    }
                    break;
                case 2:
                    if (Time.time > startingTime + 0.5f)
                    {
                        murdererText3.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;

                    }
                    break;
                case 3:
                    if (Time.time > startingTime + 0.5f)
                    {
                        murdererText4.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;

                    }
                    break;
                case 4:
                    if (Time.time > startingTime + 0.2f)
                    {
                        murdererText5.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;

                    }
                    break;
                case 5:
                    if (Time.time > startingTime + 0.2f)
                    {
                        murdererText6.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;

                    }
                    break;
                case 6:
                    if (Time.time > startingTime + 0.2f)
                    {
                        murdererText7.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;

                    }
                    break;
                case 7:
                    if (Time.time > startingTime + 0.2f)
                    {
                        murdererText8.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;

                    }
                    break;
                case 8:
                    if (Time.time > startingTime + 0.2f)
                    {
                        murdererText9.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;

                    }
                    break;
                case 9:
                    if (Time.time > startingTime + 0.2f)
                    {
                        murdererText10.SetActive(true);
                        sound.Play();
                        startingTime = Time.time;
                        stage++;

                    }
                    break;
                case 10:
                    if (Time.time > startingTime + 1f)
                    {
                        murdererText10.SetActive(false);
                        murdererText9.SetActive(false);
                        murdererText8.SetActive(false);
                        murdererText7.SetActive(false);
                        murdererText6.SetActive(false);
                        murdererText5.SetActive(false);
                        murdererText4.SetActive(false);

                        murdererText3.SetActive(false);
                        murdererText2.SetActive(false);
                        murdererText1.SetActive(false);
                        stage++;
                        manager.incrementSteps();

                    }
                    break;
            }

    }
    public void startMurderer()
    {
        startingTime = Time.time;
        started = true;
    }
}
