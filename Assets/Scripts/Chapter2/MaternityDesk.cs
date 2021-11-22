using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaternityDesk : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserInput _userInput;
    private HospitalManager manager;
    InventoryManager inventory;
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

            _userInput = FindObjectOfType<UserInput>();
            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            string fileName;
            if (!manager.dollPuzzleSolved) fileName = "/Dialogs/DeskNote.json";
            else if (!manager.hasAntibiotic) fileName = "/Dialogs/DeskGet.json";
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
                   }));
               }));

        }
    }
}

