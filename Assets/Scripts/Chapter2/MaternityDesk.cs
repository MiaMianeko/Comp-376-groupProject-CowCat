using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaternityDesk : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserController _userInput;
    private HospitalManager manager;
    InventoryManager inventory;

    [SerializeField] private GameObject noteObject;

    [SerializeField] private GameObject dollPile;
    private void Awake()
    {
        manager = FindObjectOfType<HospitalManager>();
        inventory = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;



            _userInput = FindObjectOfType<UserController>();
            _userInput.canMove = false;


            if (!manager.dollPuzzleSolved) noteObject.SetActive(true);

            else
            {

                dialogGameObject.SetActive(true);
                _dialog = FindObjectOfType<Dialog>();



                string fileName;

                if (!manager.hasAntibiotic) fileName = "/Dialogs/DeskGet.json";
                else fileName = "/Dialogs/DeskEmpty.json";

                StartCoroutine(FileReader.GetText(
                   Application.streamingAssetsPath + fileName,
                   jsonData =>
                   {
                       DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                       StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                       {
                           dialogGameObject.SetActive(false);
                           if (manager.dollPuzzleSolved && !manager.hasAntibiotic)
                           {

                               inventory.getAntibiotic();
                               manager.hasAntibiotic = true;

                           }

                           _userInput.canMove = true;
                           canInteract = true;
                       }));
                   }));
            }
        }
    }

    private void spawnDolls()
    {
        manager.dollDrop();
        dollPile.SetActive(true);

        manager.dollsSpawned = true;

        _userInput = FindObjectOfType<UserController>();
        _userInput.canMove = false;

        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();

        string fileName;

        fileName = "/Dialogs/DollSpawned.json";
        StartCoroutine(FileReader.GetText(
               Application.streamingAssetsPath + fileName,
               jsonData =>
               {
                   DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                   StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                   {
                       dialogGameObject.SetActive(false);
                       
                       _userInput.canMove = true;

                       canInteract = true;
                   }));
               }));
    }

    public void clickCloseButton()
    {
        noteObject.SetActive(false);
        _userInput.canMove = true;

        if (!manager.dollsSpawned) spawnDolls();
        else canInteract = true;
    }
}

