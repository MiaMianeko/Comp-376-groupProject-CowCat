using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PrologueHallGameManager : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] GameObject playerGameObject;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private AudioSource doorOpenAudioClip;

    void Start()
    {
        Invoke(nameof(LoadDialog1), 3.0f);
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/PrologueHallSceneDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                UserInput player = playerGameObject.GetComponent<UserInput>();
                player.canMove = false;
                StartCoroutine(OutputDialog(dialogData, nameof(ReleaseMoveLock)));
            }));
    }

    // Update is called once per frame
    void Update()
    {
        Door060 door = FindObjectOfType<Door060>();
        if (door.canInteract)
        {
            if (Input.GetKey(KeyCode.F))
            {
                doorOpenAudioClip.Play();
                Invoke(nameof(ChangeScene), 1.3f);
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
            yield return _dialog.TypeText(jsonDialogData.content);
            yield return new WaitUntil(() => Input.GetButtonDown("Skip"));
        }

        Invoke(callbackFunctionName, 0);
    }
}