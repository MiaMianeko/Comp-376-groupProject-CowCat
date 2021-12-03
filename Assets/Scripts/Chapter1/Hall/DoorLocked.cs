using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : Interactable
{

    UserController player;
    [SerializeField] GameObject dialogObject;
    private Dialog _dialog;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<UserController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            player.canMove = false;
            player.direction = Vector3.zero;
            player.isMove = false;
            dialogObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();
            dialogObject.SetActive(true);
            StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/HallwaySceneDoorLocked.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogObject.SetActive(false);
                        player.canMove = true;
                    //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    canInteract = true;
                    }));
                }));
        }
    }
}
