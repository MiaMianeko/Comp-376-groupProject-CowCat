using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class BirksSenceManager : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject flashGameObject;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private AudioSource crackAudioSource;
    public bool end;
    public Animator transition;

    void Start()
    {
        // Initialize the member variables
        end = false;
        StartCoroutine(LoadBriks());
        Invoke(nameof(LoadDialog1), 2.0f);
    }

    void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/BirksScenedialog1.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        player.canMove = false;

        // Start Play the first Dialog
        StartCoroutine(OutputDialog(dialogData1, nameof(MoveToSectary)));
    }

    // Update is calsled once per frame
    void Update()
    {
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        if (player.canTalk && player.isInteract)
        {
            dialogGameObject.SetActive(true);
            string jsonData2 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/BirksScenedialog2.json");
            DialogData dialogData2 = JsonUtility.FromJson<DialogData>(jsonData2);
            player.canTalk = false;
            StartCoroutine(OutputDialog(dialogData2, nameof(MoveToCamera)));
        }

        if (player.canTakePicture && player.isInteract)
        {
            dialogGameObject.SetActive(true);
            string jsonData3 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/BirksScenedialog3.json");
            DialogData dialogData3 = JsonUtility.FromJson<DialogData>(jsonData3);
            StartCoroutine(OutputDialog(dialogData3, nameof(TakePictures)));


            player.canTakePicture = false;
        }

        if (player.isTalked && player.gotPic && !end)
        {
            //player.isInteract = false;
            crackAudioSource.Play();
            dialogGameObject.SetActive(true);
            string jsonData4 = File.ReadAllText(Application.streamingAssetsPath + "/Dialogs/BirksScenedialog4.json");
            DialogData dialogData4 = JsonUtility.FromJson<DialogData>(jsonData4);

            StartCoroutine(OutputDialog(dialogData4, nameof(LeaveBriks)));
            end = true;
        }
    }

    IEnumerator LoadBriks()
    {
        yield return new WaitForSeconds(2);
    }

    private void LeaveBriks()
    {
        transition.SetTrigger("end");
        Invoke(nameof(ChangeScene), 2);
    }

    void TakePictures()
    {
        dialogGameObject.SetActive(false);
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        flashGameObject.SetActive(true);
        player.gotPic = true;
    }


    void ChangeScene()
    {
        dialogGameObject.SetActive(false);
        //SceneManager.LoadScene("Scenes/Prologue/");
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        player.isInteract = false;
        SceneManager.LoadScene("Scenes/Prologue/PrologueHallScene");
    }

    void MoveToSectary()
    {
        dialogGameObject.SetActive(false);
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        player.canMove = true;
    }


    void MoveToCamera()
    {
        dialogGameObject.SetActive(false);
        BirksPlayerInput player = playerGameObject.GetComponent<BirksPlayerInput>();
        player.canMove = true;
        player.isTalked = true;
        player.isInteract = false;
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
}