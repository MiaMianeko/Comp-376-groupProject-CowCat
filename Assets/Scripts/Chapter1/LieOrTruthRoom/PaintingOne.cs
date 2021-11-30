using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingOne : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserController _userInput;
    private LieOrTruthGameManager _lieOrTruthGameManager;

    private void Awake()
    {
        _lieOrTruthGameManager = FindObjectOfType<LieOrTruthGameManager>();
    }

    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;

            _userInput = FindObjectOfType<UserController>();
            _userInput.isFacingUp = true;
            _userInput.isFacingDown = false;
            _userInput.isFacingLeft = false;
            _userInput.isFacingRight = false;
            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Painting1Dialog1.json",
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
    }
}