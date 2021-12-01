using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crib : Interactable

{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserController _userInput;
    [SerializeField] private int dollNeeded;
    [SerializeField] private int cribNumber;
    private int currentDoll;
    public bool correctDoll;
    [SerializeField] GameObject inventoryObject;
    private InventoryManager inventoryManager;
    private HospitalManager manager;

    AudioSource crySound;

    [SerializeField] Sprite doll1;
    [SerializeField] Sprite doll2;
    [SerializeField] Sprite doll3;
    [SerializeField] Sprite doll4;
    [SerializeField] Sprite doll5;
    [SerializeField] Sprite doll6;
    [SerializeField] Sprite doll7;

    [SerializeField] GameObject spriteObject;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<HospitalManager>();
        _userInput = FindObjectOfType<UserController>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        currentDoll = -1;
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        crySound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            string filename = "";
            if (!manager.dollsPickedUp) filename = "CribNoDolls.json";
            else if (!manager.dollPuzzleSolved && currentDoll < 0) filename = "CribEmptyBed.json";
            else if (!manager.dollPuzzleSolved && currentDoll > 0) filename = "CribBedFull.json";
            else if (manager.dollPuzzleSolved) filename = "CribPuzzleSolved.json";

            canInteract = false;


            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();


            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/" + filename,
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);
                        if (manager.dollsPickedUp && !manager.dollPuzzleSolved)
                        {
                            if (currentDoll < 0)
                            {
                                placeDoll();
                            }
                            else takeDoll();
                        }
                        else
                        {
                            _userInput.canMove = true;
                            canInteract = true;
                        }
                    }));
                }));
        }
    }

    private void placeDoll()
    {
        manager.atCrib = true;

        manager.cribNumber = cribNumber;

        inventoryObject.SetActive(true);
    }

    private void takeDoll()
    {
        inventoryManager.pickUpDoll(currentDoll);

        currentDoll = -1;

        spriteObject.SetActive(false);

        _userInput.canMove = true;

        canInteract = true;
    }

    public void changeSprite(int dollNumber)
    {
        switch (dollNumber)
        {
            case 1:
                spriteObject.SetActive(true);
                spriteRenderer.sprite = doll1;
                break;
            case 2:
                spriteObject.SetActive(true);
                spriteRenderer.sprite = doll2;
                break;
            case 3:
                spriteObject.SetActive(true);
                spriteRenderer.sprite = doll3;
                break;
            case 4:
                spriteObject.SetActive(true);
                spriteRenderer.sprite = doll4;
                break;
            case 5:
                spriteObject.SetActive(true);
                spriteRenderer.sprite = doll5;
                break;
            case 6:
                spriteObject.SetActive(true);
                spriteRenderer.sprite = doll6;
                break;
            case 7:
                spriteObject.SetActive(true);
                spriteRenderer.sprite = doll7;
                break;
        }

        currentDoll = dollNumber;
        if (currentDoll == dollNeeded) correctDoll = true;
    }

    public void playSound(float delay)
    {
        crySound.PlayDelayed(delay);
    }

    public void stopSound()
    {
        crySound.Stop();
    }
}