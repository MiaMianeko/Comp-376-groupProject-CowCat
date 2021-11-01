using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PrologueClassroomSceneManager : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] private GameObject dialogGameObject;

    void Start()
    {
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.dataPath + "/Dialogs/PrologueClassroomDialog1.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);
        StartCoroutine(OutputDialog(dialogData1, nameof(ChangeState)));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator OutputDialog(DialogData dialogData, string callbackFunctionName)
    {
        foreach (var jsonDialogData in dialogData.data)
        {
            _dialog.SetSpeaker(jsonDialogData.speaker);
            _dialog.ClearText();
            _dialog.ShowDialog(jsonDialogData.content);
            yield return new WaitForSeconds(jsonDialogData.duration);
        }

        Invoke(callbackFunctionName, 0);
    }

    private void ChangeState()
    {
        dialogGameObject.SetActive(false);
        FindObjectOfType<ClassroomSceneUserInput>().canMove = true;
    }

    public void ShowDialog2()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        string jsonData2 = File.ReadAllText(Application.dataPath + "/Dialogs/PrologueClassroomDialog2.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData2);
        StartCoroutine(OutputDialog(dialogData1, nameof(ChangeState)));
    }
}