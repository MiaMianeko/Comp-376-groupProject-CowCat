using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class LieOrTruthFriend : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserInput _userInput;
    public int roundNumber = 1;

    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;

            _userInput = FindObjectOfType<UserInput>();
            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            switch (roundNumber)
            {
                case 1:
                    // Introduction
                    FindObjectOfType<FriendController>().isFacingUp = false;
                    FindObjectOfType<FriendController>().isFacingRight = true;
                    StartCoroutine(FileReader.GetText(
                        Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog2.json",
                        jsonData =>
                        {
                            DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                            StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                            {
                                dialogGameObject.SetActive(false);
                                _userInput.canMove = true;
                                roundNumber++;
                                FindObjectOfType<LieOrTruthGameManager>().ReleasePaintingBlock();
                                FindObjectOfType<FriendController>().isFacingRight = false;
                                FindObjectOfType<FriendController>().isFacingDown = true;
                            }));
                        }));
                    break;
                case 2:
                    // Selection Box 1
                    StartCoroutine(FileReader.GetText(
                        Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog3.json",
                        jsonData =>
                        {
                            DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                            StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                            {
                                dialogGameObject.SetActive(false);
                                FindObjectOfType<LieOrTruthGameManager>().ShowSelectionBox1();
                            }));
                        }));
                    break;
            }
        }
    }
}