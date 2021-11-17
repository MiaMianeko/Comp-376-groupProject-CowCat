using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BirksSceneManager : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject flashGameObject;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private AudioSource crackAudioSource;
    public bool end;
    public Animator transition;

    void Start()
    {
        // Initialize the member variables
        end = false;
        StartCoroutine(LoadBriks(() => LoadDialog1()));
    }

    private IEnumerator LoadBriks(Action callback)
    {
        yield return new WaitForSeconds(2);
        callback();
    }

    void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();

        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/BirksSceneDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                playerGameObject.GetComponent<BirksPlayerInput>().canMove = false;
                StartCoroutine(OutputDialog(dialogData, nameof(MoveToSectary)));
            }));
    }


    void MoveToSectary()
    {
        dialogGameObject.SetActive(false);
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        player.canMove = true;
    }

    // Update is calsled once per frame
    void Update()
    {
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        if (player.canTalk && player.isFacingUp)
        {
            dialogGameObject.SetActive(true);
            StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/BirksSceneDialog2.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    player.canTalk = false;
                    StartCoroutine(OutputDialog(dialogData, nameof(MoveToCamera)));
                }));
        }

        if (player.canTakePicture && player.isFacingUp)
        {
            dialogGameObject.SetActive(true);
            player.canTakePicture = false;
            StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/BirksSceneDialog3.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(OutputDialog(dialogData, nameof(TakePictures)));
                }));
        }

        if (player.isTalked && player.gotPic && !end)
        {
            //player.isInteract = false;
            crackAudioSource.Play();
            dialogGameObject.SetActive(true);
            StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/BirksSceneDialog4.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(OutputDialog(dialogData, nameof(LeaveBriks)));
                    end = true;
                }));
        }
    }

    private void LeaveBriks()
    {
        transition.SetTrigger("end");
        Invoke(nameof(ChangeScene), 2);
    }

    void TakePictures()
    {
        dialogGameObject.SetActive(false);
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        flashGameObject.SetActive(true);
        player.gotPic = true;
    }

    void ChangeScene()
    {
        dialogGameObject.SetActive(false);
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        player.isFacingUp = false;
        SceneManager.LoadScene("Scenes/Prologue/PrologueHallScene");
    }

    void MoveToCamera()
    {
        dialogGameObject.SetActive(false);
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        player.canMove = true;
        player.isTalked = true;
        player.isFacingUp = false;
    }

    private IEnumerator OutputDialog(DialogData dialogData, string callbackFunctionName)
    {
        foreach (var jsonDialogData in dialogData.data)
        {
            _dialog.SetSpeaker(jsonDialogData.speaker);
            _dialog.ClearText();
            yield return _dialog.TypeText(jsonDialogData.content);
            yield return new WaitUntil(() => Input.GetButtonDown("Skip"));
        }

        Invoke(callbackFunctionName, 0);
    }
}