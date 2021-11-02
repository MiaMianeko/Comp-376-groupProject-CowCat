using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PrologueHallGameManager : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] GameObject playerGameObject;
    [SerializeField] private GameObject dialogGameObject;

    void Start()
    {
        Invoke(nameof(LoadDialog1), 1.0f);
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/ProloguehallSceneDialog1.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);
        UserInput player = playerGameObject.GetComponent<UserInput>();
        player.canMove = false;
        StartCoroutine(OutputDialog(dialogData1, nameof(ReleaseMoveLock)));
    }

    // Update is called once per frame
    void Update()
    {
        Door060 door = FindObjectOfType<Door060>();
        if (door.canInteract)
        {
            if (Input.GetKey(KeyCode.F))
            {
                ChangeScene();
            }
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Scenes/Prologue/ClassroomScene");
    }

    private void ReleaseMoveLock()
    {
        UserInput player = playerGameObject.GetComponent<UserInput>();
        player.canMove = true;
        dialogGameObject.SetActive(false);
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
}