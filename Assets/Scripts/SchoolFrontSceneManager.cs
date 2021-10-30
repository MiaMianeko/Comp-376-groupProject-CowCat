using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class SchoolFrontSceneManager : MonoBehaviour
{
    private Dialog _dialog;
    private bool _canOpenBag;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject bagGameObject;

    public SchoolFrontSceneManager()
    {
        _canOpenBag = false;
    }

    void Start()
    {
        // Initialize the member variables
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.dataPath + "/Dialogs/SchoolFrontDialog1.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);

        // Start Play the first Dialog
        StartCoroutine(OutputDialog(dialogData1, nameof(OpenBagAsync)));
    }

    // Update is calsled once per frame
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
            _dialog.ShowDialog(jsonDialogData.content);
            yield return new WaitForSeconds(jsonDialogData.duration);
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
        dialogGameObject.SetActive(true);
        string jsonData2 = File.ReadAllText(Application.dataPath + "/Dialogs/SchoolFrontDialog2.json");
        DialogData dialogData2 = JsonUtility.FromJson<DialogData>(jsonData2);
        // Start Play the first Dialog
        StartCoroutine(OutputDialog(dialogData2, nameof(ChangeToBirksSence)));
    }

    private void ChangeToBirksSence()
    {
        SceneManager.LoadScene("Scenes/Prologue/BirksScene");
    }
}