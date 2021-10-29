using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SchoolFrontSceneManager : MonoBehaviour
{
    private Dialog _dialog;

    void Start()
    {
        // Initialize the member variables
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.dataPath + "/Dialogs/dialog1.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);


        // Start Play the first Dialog
        StartCoroutine(OutputDialog(dialogData1, nameof(ToDoNext)));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator OutputDialog(DialogData dialogData, string callbackFunctionName)
    {
        foreach (var jsonDialogData in dialogData.data)
        {
            _dialog.ClearText();
            _dialog.ShowDialog(jsonDialogData.content);
            yield return new WaitForSeconds(jsonDialogData.duration);
        }

        Invoke(callbackFunctionName, 0);
    }

    private void ToDoNext()
    {
        print(111);
    }
}