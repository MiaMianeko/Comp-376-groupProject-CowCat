using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
        StartCoroutine(LoadLevel());
        Invoke(nameof(LoadDialog1), 2.0f);
    }

    public void LoadDialog1()
    {
        // Initialize the member variables
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/SchoolFrontDialog1.json");
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
            yield return _dialog.TypeText(jsonDialogData.content);
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
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
        string jsonData2 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/SchoolFrontDialog2.json");
        DialogData dialogData2 = JsonUtility.FromJson<DialogData>(jsonData2);
        // Start Play the first Dialog
        StartCoroutine(OutputDialog(dialogData2, nameof(LeaveScene)));
    }

    private void ChangeToBirksSence()
    {
        SceneManager.LoadScene("Scenes/Prologue/BirksScene");
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2.0f);
    }

    public void LeaveScene()
    {
        transition.SetTrigger("End");
        Invoke(nameof(ChangeToBirksSence), 3);
    }
}