using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SchoolFrontSceneManager : MonoBehaviour
{
    public Animator transition;
    private Dialog _dialog;
    private bool _canOpenBag;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject bagGameObject;
    [SerializeField] private GameObject noteGameObject;

    public SchoolFrontSceneManager()
    {
        _canOpenBag = false;
    }

    void Start()
    {
        StartCoroutine(LoadLevel(() => LoadDialog1()));
    }

    private void LoadDialog1()
    {
        // Initialize the member variables
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();

        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/SchoolFrontDialog1.json",
            jsonData1 =>
            {
                DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);
                StartCoroutine(OutputDialog(dialogData1, nameof(OpenBagAsync)));
            }));
    }

    void Update()
    {
        if (Input.GetButtonDown("Bag") && _canOpenBag)
        {
            dialogGameObject.SetActive(false);
            bagGameObject.SetActive(true);
        }
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

    private void OpenBagAsync()
    {
        _canOpenBag = true;
    }

    public void ReadNote()
    {
        bagGameObject.SetActive(false);
        dialogGameObject.SetActive(false);
        noteGameObject.SetActive(true);
    }

    public void LoadDialog2()
    {
        noteGameObject.SetActive(false);
        dialogGameObject.SetActive(true);

        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/SchoolFrontDialog2.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(LeaveScene)));
            }));
    }

    private void ChangeToBirksSence()
    {
        SceneManager.LoadScene("Scenes/Prologue/BirksScene");
    }

    IEnumerator LoadLevel(Action callback)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2.0f);
        callback();
    }

    public void LeaveScene()
    {
        transition.SetTrigger("End");
        Invoke(nameof(ChangeToBirksSence), 3);
    }
}