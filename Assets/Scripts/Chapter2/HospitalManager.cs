using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (endTriggered)
        {
            switch (endStage)
            {
                case 0: 
                    flashlight.SetActive(false);
                    if (Time.time > endTriggeredTime + 0.5f)
                    {
                        flashlight.SetActive(true);
                        endSpawnObject1.SetActive(true);
                        endStage = 1;
                        endTriggeredTime = Time.time;
                    }
                    break;
                case 1:
                    if (Time.time > endTriggeredTime + 1.0f)
                    {
                        flashlight.SetActive(false);
                        endSpawnObject1.SetActive(false);
                        endStage = 2;
                        endTriggeredTime = Time.time;
                    }
                    break;
                case 2:
                    if (Time.time > endTriggeredTime + 0.5f)
                    {
                        flashlight.SetActive(true);
                        endSpawnObject2.SetActive(true);
                        endStage = 3;
                        endTriggeredTime = Time.time;
                    }
                    break;
                case 3:
                    if (Time.time > endTriggeredTime + 1.0f)
                    {
                        flashlight.SetActive(false);
                        endSpawnObject2.SetActive(false);
                        endStage = 4;
                        endTriggeredTime = Time.time;
                    }
                    break;
                case 4:
                    if (Time.time > endTriggeredTime + 0.5f)
                    {
                        flashlight.SetActive(true);
                        endSpawnObject3.SetActive(true);
                        endStage = 5;
                        endTriggeredTime = Time.time;
                    }
                    break;
                case 5:
                    if (Time.time > endTriggeredTime + 1.0f)
                    {
                        flashlight.SetActive(false);
                        

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

        /*
        END CHAPTER FUNCTION GOES BRR!!
        1. Overlay of quick animation showing bottle falling and breaking (multiframes, like story book).
        2. Player saying she remembers now... This all happened before...
        3. Friend appears from shadows to taunt player, light flickers as more friends spawn with bloody faces.
        4. Light flickers to black as final scene is loaded

        */
    }
}
