using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickFriend : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserController _userInput;
    private HospitalManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<HospitalManager>();
    }

    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;

            _userInput = FindObjectOfType<UserController>();
            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            string fileName = "/Dialogs/CheckOnFriend.json";


            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + fileName,
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);
                        canInteract = true;
                        _userInput.canMove = true;
                    }));
                }));
        }
    }
}