using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineObject : Interactable
{

    UserInput player;
    HospitalManager manager;
    [SerializeField] GameObject machinePuzzle;
    Dialog _dialog;

    [SerializeField] GameObject dialogGameObject;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<HospitalManager>();
        player = FindObjectOfType<UserInput>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            if (!manager.machinePuzzleSolved)
            {
                canInteract = false;
                machinePuzzle.SetActive(true);
                player.canMove = false;
            }
            else if (!(manager.hasAntibiotic && manager.hasLiver))
            {

                dialogGameObject.SetActive(true);
                _dialog = FindObjectOfType<Dialog>();

                StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/MachineMissingIngredient.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);

                        player.canMove = true;
                    }));
                }));
            }
            else
            {
                manager.endChapter();
            }


            
        }
    }

    public void closePuzzle()
    {
        canInteract = true;
        machinePuzzle.SetActive(false);
        player.canMove = true;
    }
}
