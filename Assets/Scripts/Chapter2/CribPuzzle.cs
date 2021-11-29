using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CribPuzzle : MonoBehaviour
{
    [SerializeField] Crib crib1;
    [SerializeField] Crib crib2;
    [SerializeField] Crib crib3;
    [SerializeField] Crib crib4;
    [SerializeField] Crib crib5;
    [SerializeField] Crib crib6;
    [SerializeField] Crib crib7;

    HospitalManager manager;

    [SerializeField] GameObject dialogGameObject;

    InventoryManager inventory;

    AudioSource drawerUnlock;

    UserInput _userInput;
    Dialog _dialog;
    public bool solved;
    // Start is called before the first frame update
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        manager = FindObjectOfType<HospitalManager>();
        inventory = FindObjectOfType<InventoryManager>();
        drawerUnlock = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.dollPuzzleSolved && crib1.correctDoll && crib2.correctDoll && crib3.correctDoll && crib4.correctDoll && crib5.correctDoll && crib6.correctDoll && crib7.correctDoll)
        {
            solved = true;
            manager.dollPuzzleSolved = true;

            initiateCry();

            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();





            StartCoroutine(FileReader.GetText(
               Application.streamingAssetsPath + "/Dialogs/DollPuzzleSolved.json",
               jsonData =>
               {
                   DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                   StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                   {
                       dialogGameObject.SetActive(false);

                       stopCry();
                       _userInput.canMove = true;
                   }));
               }));

        }
    }
    private void initiateCry()
    {
        inventory.changeBGMVolume(0.2f);
        crib1.playSound(0f);

        crib3.playSound(0.05f);

        crib5.playSound(0.1f);

        crib7.playSound(0.15f);


    }
    private void stopCry()
    {

        crib1.stopSound();
        crib2.stopSound();
        crib3.stopSound();
        crib4.stopSound();
        crib5.stopSound();
        crib6.stopSound();
        crib7.stopSound();
        drawerUnlock.Play();
        inventory.changeBGMVolume(0.6f);
    }
}
