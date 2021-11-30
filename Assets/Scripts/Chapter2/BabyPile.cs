using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPile : Interactable
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserController _userInput;

    private HospitalManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<HospitalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;

            _userInput = FindObjectOfType<UserController>();
            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();


            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/BabyPile.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);
                        _userInput.canMove = true;
                        this.gameObject.SetActive(false);
                        manager.pickUpDolls();
                    }));
                }));
        }
    }
}