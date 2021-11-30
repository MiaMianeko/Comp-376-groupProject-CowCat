using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRoomFriend : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject selectionBoxObject;
    private Dialog _dialog;
    private UserController _userInput;
    ToyRoomGameManager manager;

    private void Start()
    {
        _userInput = FindObjectOfType<UserController>();
        manager = FindObjectOfType<ToyRoomGameManager>();
    }

    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            _userInput.canMove = false;
            canInteract = false;
            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            string filename;
            if (manager.progress.beenThereBefore) filename = "/Dialogs/Chapter1ToyRoomDialog3Alt.json";
            else filename = "/Dialogs/Chapter1ToyRoomDialog3.json";

            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + filename,
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);

                        selectionBoxObject.SetActive(true);
                    }));
                }));
        }
    }
}