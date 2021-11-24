using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingFour : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserInput _userInput;
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

            _userInput = FindObjectOfType<UserInput>();
            _userInput.isFacingUp = true;
            _userInput.isFacingDown = false;
            _userInput.isFacingLeft = false;
            _userInput.isFacingRight = false;
            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();


            switch (_lieOrTruthGameManager.roundNumber)
            {
                case 1:
                    StartCoroutine(FileReader.GetText(
                        Application.streamingAssetsPath + "/Dialogs/Painting4Dialog1.json",
                        jsonData =>
                        {
                            DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                            StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                            {
                                dialogGameObject.SetActive(false);
                                _userInput.canMove = true;
                            }));
                        }));
                    break;
                case 2:
                    StartCoroutine(FileReader.GetText(
                        Application.streamingAssetsPath + "/Dialogs/Painting4Dialog2.json",
                        jsonData =>
                        {
                            DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                            StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                            {
                                dialogGameObject.SetActive(false);
                                _userInput.canMove = true;
                            }));
                        }));
                    break;
                case 3:
                    StartCoroutine(FileReader.GetText(
                        Application.streamingAssetsPath + "/Dialogs/Painting4Dialog3.json",
                        jsonData =>
                        {
                            DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                            StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                            {
                                dialogGameObject.SetActive(false);
                                _userInput.canMove = true;
                            }));
                        }));
                    break;
            }
        }
    }
}