using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : Interactable
{
    [SerializeField] private GameObject newspaper;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject button;
    private Dialog _dialog;
    private UserController _userInput;
    // Start is called before the first frame update
    void Start()
    {
        newspaper.SetActive(false);
        _userInput = FindObjectOfType<UserController>();
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F)&& !isInteracted)
        {
            canInteract = false;
            isInteracted = true;
            newspaper.SetActive(true);
            LoadDialogueNews();
            

        }

        if (canInteract && Input.GetKey(KeyCode.F) && isInteracted)
        {
            canInteract = false;
            newspaper.SetActive(true);
            _userInput.canMove = false;
            button.SetActive(true);
        }
    }

    
    public void LoadDialogueNews()
    {
        _userInput.canMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserController>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/HospitalNewspaper.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    button.SetActive(true);
                }));
            }));
    }
    public void CloseNewspaper()
    {
        newspaper.SetActive(false);
        _userInput.canMove = true;
    }

    
}
