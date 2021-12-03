using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class RevelationsManager : MonoBehaviour
{
    [SerializeField] GameObject giantHead;
    bool giantHeadMoving;
    [SerializeField] float giantHeadHeight;
    [SerializeField] float giantHeadSpeed;
    int stepsOfEnding = 0;
    bool talking;
    Dialog _dialog;
    float timer;
    [SerializeField] GameObject dialogGameObject;
    [SerializeField] GameObject faultText;
    [SerializeField] GameObject cowardText;
    AudioSource audio;
    [SerializeField] AudioClip stinger;
    [SerializeField] AudioClip stomp;
    [SerializeField] AudioClip monsterScream;
    FriendController friendController;
    MurdererManager murdererManager;

    [SerializeField] GameObject livPic;
    [SerializeField] GameObject levPic;
    [SerializeField] GameObject wholePic;

    [SerializeField] GameObject friendGameObject;

    [SerializeField] GameObject FindTheLiars;

    [SerializeField] GameObject goodGameTrigger;
    [SerializeField] GameObject badEndingMonster;
    [SerializeField] GameObject goodEndingMonster;

    [SerializeField] GameObject blackScreen;
    

    UserController player;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        murdererManager = FindObjectOfType<MurdererManager>();
        friendController = FindObjectOfType<FriendController>();
        player = FindObjectOfType<UserController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!talking)
        switch (stepsOfEnding)
        {
            case 0:
                    playEndGameDialog("Revelations1.json");
                    break;

            case 1:
                giantHead.transform.Translate(Vector3.up * Time.deltaTime * giantHeadSpeed);
                    audio.PlayOneShot(stomp);
                if (giantHeadHeight < giantHead.transform.position.y) stepsOfEnding = 2;
                break;
            case 2:
                playEndGameDialog("Revelations2.json");
                break;
            case 3:
                faultText.SetActive(true);
                    audio.PlayOneShot(stinger);
                    stepsOfEnding++;
                    timer = Time.time;
                    break;
                case 4:
                    if (Time.time > timer + 1.5f)
                    {
                        faultText.SetActive(false);
                        timer = Time.time;
                        stepsOfEnding++;
                    }
                    break;
                case 5:
                    if(Time.time > timer +0.25f)
                        playEndGameDialog("Revelations3.json");
                    break;
                case 6:
                    cowardText.SetActive(true);
                    audio.PlayOneShot(stinger);
                    stepsOfEnding++;
                    timer = Time.time;
                    break;
                case 7:
                    if (Time.time > timer + 1.5f)
                    {
                        stepsOfEnding++;
                        cowardText.SetActive(false);
                    }

                    break;
                case 8:

                    if (Time.time > timer + 0.25f)
                        playEndGameDialog("Revelations4.json");
                    break;

                case 9:
                    murdererManager.startMurderer();
                    stepsOfEnding++;
                    break;
                //case 10:
                case 11:
                    if(Time.time > timer + 0.5f)
                        playEndGameDialog("Revelations5.json");

                    break;
                case 12:
                    FindTheLiars.SetActive(true);
                    stepsOfEnding++;
                    break;
                //case 13
                case 14:
                    FindTheLiars.SetActive(false);
                    if (Time.time > timer+0.5)
                        playEndGameDialog("Revelations6.json");
                    break;
                case 15:
                    StartCoroutine(FriendComeIn(() => { playEndGameDialog("Revelations7.json"); }));
                    stepsOfEnding++;
                    break;
                //case 16:
                case 17:
                    livPic.SetActive(true);
                    playEndGameDialog("Revelations8.json");
                    break;
                case 18:
                    livPic.SetActive(false);
                    levPic.SetActive(true);
                    playEndGameDialog("Revelations9.json");
                    break;
                case 19:
                    wholePic.SetActive(true);
                    levPic.SetActive(false);
                    playEndGameDialog("Revelations10.json");
                    break;
                case 20:
                    wholePic.SetActive(false);
                    playEndGameDialog("Revelations11.json");
                    break;
                case 21:
                    goodGameTrigger.SetActive(true);
                    playEndGameDialog("Revelations12.json");
                    break;
                case 22:
                    player.canMove = true;
                    break;
                case 23:
                    playEndGameDialog("Revelations13.json");
                    break;
                case 24:
                    badEndingMonster.SetActive(true);
                    goodGameTrigger.SetActive(false);
                    badEndingMonster.transform.Translate(Vector3.left * Time.deltaTime * (giantHeadSpeed*3));
                    if(badEndingMonster.transform.position.x < -0.5f) stepsOfEnding++; 
                    break;
                case 25:
                    blackScreen.SetActive(true);
                    audio.PlayOneShot(monsterScream);
                    timer = Time.time;
                    stepsOfEnding++;
                    break;
                case 26:
                    if(Time.time > timer + 1)
                    SceneManager.LoadScene("Scenes/Epilogue/BE/ChasingScene");
                    break;
                case 50:
                    playEndGameDialog("Revelations14.json");
                    break;
                case 51:
                    goodEndingMonster.SetActive(true);
                    friendGameObject.SetActive(false);
                    goodEndingMonster.transform.Translate(Vector3.right * Time.deltaTime * (giantHeadSpeed * 3));
                    if (goodEndingMonster.transform.position.x > 2.1f) stepsOfEnding++;
                    break;
                case 52:
                    blackScreen.SetActive(true);
                    audio.PlayOneShot(monsterScream);
                    timer = Time.time;
                    stepsOfEnding++;
                    break;
                case 53:
                    if (Time.time > timer + 1)
                        SceneManager.LoadScene("Scenes/Epilogue/TE/ChasingScene");
                    break;
            }
    }

    private void playEndGameDialog(string filename)
    {
        talking = true;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/" + filename,
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    stepsOfEnding += 1;
                    talking = false;
                }));
            }));
    }
    public void incrementSteps()
    {
        stepsOfEnding++;
        timer = Time.time;
    }
    public IEnumerator FriendComeIn(Action callback)
    {
        friendController.direction = Vector3.right;
        yield return new WaitUntil(() => friendGameObject.GetComponent<Rigidbody2D>().position.x > -4);
        friendController.direction = Vector3.zero;
        callback();
    }
    public void chooseBadEnding()
    {
        player.canMove = false;
        stepsOfEnding++;
        timer = Time.time;

    }
    public void chooseGoodEnding()
    {
        player.canMove = false;
        stepsOfEnding = 50;
        timer = Time.time;
    }

}


