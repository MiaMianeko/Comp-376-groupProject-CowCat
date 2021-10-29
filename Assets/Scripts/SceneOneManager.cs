using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SceneOneManager : MonoBehaviour
{
    private Dialog _dialog;

    void Start()
    {
        _dialog = FindObjectOfType<Dialog>();
        string jsonData = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/dialog1.json");
        DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);

        StartCoroutine(OutputDialog(dialogData));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator OutputDialog(DialogData dialogData)
    {
        foreach (var jsonDialogData in dialogData.data)
        {
            _dialog.ClearText();
            _dialog.ShowDialog(jsonDialogData.content);
            yield return new WaitForSeconds(jsonDialogData.duration);
        }
    }
}