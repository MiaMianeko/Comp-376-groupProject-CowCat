using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PrologueClassroomSceneManager : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject playerInClassGameObject;
    [SerializeField] private GameObject playerGameObject;
    public Animator transition;

    void Start()
    {
        Invoke(nameof(LoadDialog1), 1.0f);
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/PrologueClassroomDialog1.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);
        StartCoroutine(OutputDialog(dialogData1, nameof(ChangeState)));
    }

    private IEnumerator OutputDialog(DialogData dialogData, string callbackFunctionName)
    {
        foreach (var jsonDialogData in dialogData.data)
        {
            _dialog.SetSpeaker(jsonDialogData.speaker);
            _dialog.ClearText();
            yield return _dialog.TypeText(jsonDialogData.content);
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        }

        Invoke(callbackFunctionName, 0);
    }

    private void ChangeState()
    {
        dialogGameObject.SetActive(false);
        FindObjectOfType<UserInput>().canMove = true;
    }

    public void ShowDialog2()
    {
        Destroy(playerGameObject);
        playerInClassGameObject.SetActive(true);
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        FindObjectOfType<UserInput>().canMove = false;
        string jsonData2 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/PrologueClassroomDialog2.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData2);
        StartCoroutine(OutputDialog(dialogData1, nameof(Sleep)));
    }

    private void Sleep()
    {
        transition.SetTrigger("End");
        Invoke(nameof(SwitchToNextScene), 2);
    }

    public void SwitchToNextScene()
    {
        SceneManager.LoadScene("Scenes/Chapter1/ClassroomScene");
    }
}