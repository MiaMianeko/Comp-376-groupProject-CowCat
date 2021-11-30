using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueClassroomSceneManager : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject playerInClassGameObject;
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject backgroundGameObject;
    public Animator transition;

    void Start()
    {
        Invoke(nameof(LoadDialog1), 1.0f);
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();

        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/PrologueClassroomDialog1.json",
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
        FindObjectOfType<UserController>().canMove = true;
    }

    public void ShowDialog2()
    {
        backgroundGameObject.GetComponent<AudioSource>().Pause();
        Destroy(playerGameObject);
        playerInClassGameObject.SetActive(true);
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        FindObjectOfType<UserController>().canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/PrologueClassroomDialog2.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(OutputDialog(dialogData, nameof(Sleep)));
            }));
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