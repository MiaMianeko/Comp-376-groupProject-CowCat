using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgueBed : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserInput _userInput;
    [SerializeField] LiverExtractionGame puzzles;

    private void Awake()
    {

    }

    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;

            _userInput = FindObjectOfType<UserInput>();
            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();


            
            StartCoroutine(FileReader.GetText(
               Application.streamingAssetsPath + "/Dialogs/HospitalMorgueCorpseCheck.json",
               jsonData =>
               {
                   DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                   StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                      {
                         dialogGameObject.SetActive(false);
                          puzzles.StartGame();
                      }));
               }));
             
        }
    }
}
