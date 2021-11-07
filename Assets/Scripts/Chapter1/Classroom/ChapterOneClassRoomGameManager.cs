using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneClassRoomGameManager : MonoBehaviour
{
    private UserInput _userInput;

    private Dialog _dialog;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject playerGameObject;
    public Animator transition;
    public bool isChangeScene;

    public ChapterOneClassRoomGameManager()
    {
        isChangeScene = false;
    }

    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        Invoke(nameof(LoadDialog1), 1.0f);
    }

    // Start is called before the first frame update
    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ClassroomDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(ChangeState)));
            }));
    }

    public void LoadDialog2()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ClassroomDialog2.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(ChangeState)));
            }));
    }

    public void LoadDialog3()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ClassroomDialog3.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(ChangeState)));
            }));
    }

    public void LoadDialog4()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ClassroomDialog4.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(ChangeState)));
            }));
    }

    public void LoadDialog5()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ClassroomDialog5.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(ChangeState)));
            }));
    }

    public void LoadDialog6()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ClassroomDialog6.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(ChangeState)));
            }));
    }

    public void LoadDialog7()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ClassroomDialog7.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(ChangeState)));
            }));
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

    private void ChangeState()
    {
        dialogGameObject.SetActive(false);
        _userInput.canMove = true;
    }
}