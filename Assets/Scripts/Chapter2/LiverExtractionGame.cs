using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiverExtractionGame : MonoBehaviour
{
    [SerializeField] CutLine round1Game;
    [SerializeField] GameObject round1Object;

    [SerializeField] BugPuzzle round2Game;
    [SerializeField] GameObject round2Object;


    [SerializeField] CutLine round3Game1;
    [SerializeField] CutLine round3Game2;
    [SerializeField] GameObject round3Object;

    [SerializeField] GameObject dialogObject;

    [SerializeField] Texture2D scalpelCursor;


    Dialog _dialog;

    HospitalManager manager;

    public int round;

    InventoryManager inventory;

    private UserController _userInput;

    MorgueBed bedClassObject;

    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        _userInput = FindObjectOfType<UserController>();
        manager = FindObjectOfType<HospitalManager>();
        inventory = FindObjectOfType<InventoryManager>();
        bedClassObject = FindObjectOfType<MorgueBed>();
    }

    // Update is called once per frame
    void Update()
    {
        if (round == 1 && round1Game.solved)
        {
            round++;
            round1Object.SetActive(false);
            round2Object.SetActive(true);




            dialogObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            string fileName = "/Dialogs/BugLevel.json";


            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + fileName,
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogObject.SetActive(false);

                    }));
                }));
        }
    
        else if (round == 2 && round2Game.solved)
        {
            round++;
            round2Object.SetActive(false);
            round3Object.SetActive(true);
        }
        else if (round == 3 && round3Game1.solved && round3Game2.solved)
        {
            round++;

            manager.hasLiver = true;

            inventory.getLiver();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);


            _userInput = FindObjectOfType<UserController>();
            _userInput.canMove = false;

            dialogObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            string fileName = "/Dialogs/LiverPickUp.json";


            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + fileName,
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogObject.SetActive(false);
                        round3Object.SetActive(false);
                        _userInput.canMove = true;
                        bedClassObject.canInteract = true;
                    }));
                }));
        }
    }

    public void StartGame()
    {
        round = 1;
        round1Object.SetActive(true);
        Cursor.SetCursor(scalpelCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}