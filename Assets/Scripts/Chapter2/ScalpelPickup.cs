using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalpelPickup : Interactable
{
    [SerializeField] GameObject dialogObject;
    HospitalManager manager;
    InventoryManager inventory;
    UserController player;

    Dialog _dialog;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        manager = FindObjectOfType<HospitalManager>();
        player = FindObjectOfType<UserController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;


            player.canMove = false;

            dialogObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            string fileName = "/Dialogs/PickUpScalpel.json";


            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + fileName,
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogObject.SetActive(false);

                        player.canMove = true;

                        this.gameObject.SetActive(false);

                        inventory.getScalpel();

                        manager.pickUpScalpel();
                    }));
                }));
        }
    }
}