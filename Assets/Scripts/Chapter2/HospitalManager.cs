using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HospitalManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogGameObject;
    private UserInput _userInput;
    private Dialog _dialog;
    public bool dollsPickedUp;
    public bool dollsSpawned;
    public bool dollPuzzleSolved;
    public bool hasAntibiotic;
    public bool hasScalpel;
    public bool hasLiver;
    public bool machinePuzzleSolved;
    public InventoryManager inventory;

    [SerializeField] GameObject flashlight;

    [SerializeField] GameObject endPanel1;
    [SerializeField] GameObject endPanel2;
    [SerializeField] GameObject endPanel3;
    [SerializeField] GameObject endPanel4;

    bool endDialogDone;

    [SerializeField] GameObject endSpawnObject1;
    [SerializeField] GameObject endSpawnObject2;
    [SerializeField] GameObject endSpawnObject3;

    bool endTriggered;
    float endTriggeredTime;
    int endStage = 0;

    AudioSource audio;

    [SerializeField] AudioClip cockroachKill;
    [SerializeField] AudioClip scalpelPickUp;
    [SerializeField] AudioClip dollSpawn;
    [SerializeField] AudioClip heartbeatSound;
    [SerializeField] AudioClip bottleBreak;


    public bool atCrib;
    public int cribNumber;

    public int dollSelected;
    

    // Start is called before the first frame update
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        Invoke(nameof(LoadDialog1), 1.0f);
        inventory = FindObjectOfType<InventoryManager>();
        cribNumber = -1;
        audio = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (endTriggered && endDialogDone)
        {
            switch (endStage)
            {

                case 0:
                    if (Time.time > endTriggeredTime + 1f)
                    {
                        endPanel1.SetActive(false);
                        endPanel2.SetActive(true);
                        audio.PlayOneShot(heartbeatSound);
                        endTriggeredTime = Time.time;
                        endStage = 1;
                    }
                    break;
                case 1:
                    if (Time.time > endTriggeredTime + 1f)
                    {
                        endPanel2.SetActive(false);
                        endPanel3.SetActive(true);
                        audio.PlayOneShot(heartbeatSound);
                        endTriggeredTime = Time.time;
                        endStage = 2;
                    }
                    break;
                case 2:
                    if (Time.time > endTriggeredTime + 1f)
                    {
                        endPanel3.SetActive(false);
                        endPanel4.SetActive(true);
                        audio.PlayOneShot(bottleBreak);
                        endTriggeredTime = Time.time;
                        endStage = 3;
                    }
                    break;

                case 3:
                    if (Time.time > endTriggeredTime + 1f)
                    {
                        endPanel3.SetActive(false);
                        endPanel4.SetActive(true);
                        audio.PlayOneShot(bottleBreak);
                        endTriggeredTime = Time.time;
                        endStage = 4;
                    }
                    break;
                case 4:
                    if (Time.time > endTriggeredTime + 1f)
                    {
                        endPanel4.SetActive(false);
                        endDialogDone = false;
                        playEndGameDialog("EndStage4.json");
                    }
                    break;
                case 14:
                    endTriggeredTime = Time.time;
                    endStage = 5;
                    break;
                case 5:
                    if (Time.time > endTriggeredTime + 0.5f)
                    {
                        flashlight.SetActive(false);
                        endStage = 6;
                        endTriggeredTime = Time.time;
                    }
                    break;
                case 6:
                    if (Time.time > endTriggeredTime + 0.25f)
                    {
                        flashlight.SetActive(true);
                        endSpawnObject1.SetActive(true);
                        endDialogDone = false;
                        playEndGameDialog("EndStage6.json");
                    }
                    break;
                case 16:
                    endTriggeredTime = Time.time;
                    endStage = 7;
                    break;
                case 7:
                    if (Time.time > endTriggeredTime + 1.0f)
                    {
                        flashlight.SetActive(false);
                        endSpawnObject1.SetActive(false);
                        endStage = 8;
                        endTriggeredTime = Time.time;
                    }
                    break;
                case 8:
                    if (Time.time > endTriggeredTime + 0.25f)
                    {
                        flashlight.SetActive(true);
                        endSpawnObject2.SetActive(true);
                        endDialogDone = false;
                        playEndGameDialog("EndStage8.json");
                    }
                    break;
                case 18:
                    endTriggeredTime = Time.time;
                    endStage = 9;
                    break;
                case 9:
                    if (Time.time > endTriggeredTime + 1.0f)
                    {
                        flashlight.SetActive(false);
                        endSpawnObject2.SetActive(false);
                        endStage = 10;
                        endTriggeredTime = Time.time;

                    }
                    break;
                case 10:
                    if (Time.time > endTriggeredTime + 0.25f)
                    {
                        flashlight.SetActive(true);
                        endSpawnObject3.SetActive(true);
                        endStage = 11;
                        endTriggeredTime = Time.time;
                    }
                    break;
                case 11:
                    if (Time.time > endTriggeredTime + 1f)
                    {
                        flashlight.SetActive(false);
                        endStage = 12;
                    }
                    break;
                case 12:
                    if (Time.time > endTriggeredTime + 0.5f)
                    {
                       // SceneManager.LoadScene("");
                    }
                    break;
            }

        }
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/HospitalEnter.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _userInput.canMove = true;
                }));
            }));
    }

    public void pickUpDolls()
    {
        dollsPickedUp = true;
        inventory.pickUpAllDolls();
    }

    public void killCockroach()
    {
        audio.PlayOneShot(cockroachKill);
    }

    public void pickUpScalpel()
    {
        audio.PlayOneShot(scalpelPickUp);
    }
    public void dollDrop()
    {
        audio.PlayOneShot(dollSpawn);
    }
    public void endChapter()
    {
        endTriggered = true;
        endTriggeredTime = Time.time;

        audio.PlayOneShot(heartbeatSound);
        endPanel1.SetActive(true);
        _userInput.canMove = false;

        endDialogDone = true;
        /*
        END CHAPTER FUNCTION GOES BRR!!
        1. Overlay of quick animation showing bottle falling and breaking (multiframes, like story book).
        2. Player saying she remembers now... This all happened before...
        3. Friend appears from shadows to taunt player, light flickers as more friends spawn with bloody faces.
        4. Light flickers to black as final scene is loaded

        */
    }
    private void playEndGameDialog(string filename)
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/" + filename,
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    endStage +=10 ;
                    endDialogDone = true;
                }));
            }));
    }
}


