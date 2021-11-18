using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalpelItem : Item
{

    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserInput _userInput;
    [SerializeField] UserInput player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void examineItem()
    {
        //Check if player is overlapping with MorgueCorpse
        if (player.canInteract) Debug.Log("");


        else

            _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;

        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();



        StartCoroutine(FileReader.GetText(
           Application.streamingAssetsPath + "/Dialogs/HospitalScalpelItemExamine.json",
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

