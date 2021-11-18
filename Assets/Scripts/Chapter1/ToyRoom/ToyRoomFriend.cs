using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRoomFriend : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject selectionBoxObject;
    private Dialog _dialog;
    private UserInput _userInput;

    private void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
    }

    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();
            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog3.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);
                        _userInput.canMove = true;
                        selectionBoxObject.SetActive(true);
                    }));
                }));
        }
    }
}