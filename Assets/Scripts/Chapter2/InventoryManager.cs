using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] UserController player;
    bool isOpen;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;
    [SerializeField] GameObject item4;
    [SerializeField] GameObject item5;
    [SerializeField] GameObject item6;
    [SerializeField] GameObject item7;
    [SerializeField] GameObject item8;
    [SerializeField] GameObject item9;
    [SerializeField] GameObject item10;

    [SerializeField] GameObject item1Image;
    [SerializeField] GameObject item2Image;
    [SerializeField] GameObject item3Image;
    [SerializeField] GameObject item4Image;
    [SerializeField] GameObject item5Image;
    [SerializeField] GameObject item6Image;
    [SerializeField] GameObject item7Image;
    [SerializeField] GameObject item8Image;
    [SerializeField] GameObject item9Image;
    [SerializeField] GameObject item10Image;

    [SerializeField] GameObject dialogGameObject;

    [SerializeField] Crib crib1;
    [SerializeField] Crib crib2;
    [SerializeField] Crib crib3;
    [SerializeField] Crib crib4;
    [SerializeField] Crib crib5;
    [SerializeField] Crib crib6;
    [SerializeField] Crib crib7;

    AudioSource bgm;

    HospitalManager manager;
    Dialog _dialog;

    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();
        manager = FindObjectOfType<HospitalManager>();
        _dialog = FindObjectOfType<Dialog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.canMove && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            inventory.SetActive(true);
            isOpen = true;
            player.canMove = false;
        }
        else if (isOpen && Input.GetKeyDown(KeyCode.E) && !manager.atCrib) closeInventory();
    }

    public void closeInventory()
    {
        player.canMove = true;
        isOpen = false;
        inventory.SetActive(false);
        manager.atCrib = false;
    }

    public void pickUpAllDolls()
    {
        item3.SetActive(true);
        item4.SetActive(true);
        item5.SetActive(true);
        item6.SetActive(true);
        item7.SetActive(true);
        item8.SetActive(true);
        item9.SetActive(true);
    }

    public void pickUpDoll(int d)
    {
        switch (d)
        {
            case 1:
                item8.SetActive(true);
                break;
            case 2:
                item9.SetActive(true);
                break;
            case 3:
                item4.SetActive(true);
                break;
            case 4:
                item5.SetActive(true);
                break;
            case 5:
                item7.SetActive(true);
                break;
            case 6:
                item6.SetActive(true);
                break;
            case 7:
                item3.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void clickOnDoll(int dollNumber)
    {
        if (!manager.atCrib)
        {
            switch (dollNumber)
            {
                case 1:
                    item8Image.SetActive(true);
                    break;
                case 2:
                    item9Image.SetActive(true);
                    break;
                case 3:
                    item4Image.SetActive(true);
                    break;
                case 4:
                    item5Image.SetActive(true);
                    break;
                case 5:
                    item7Image.SetActive(true);
                    break;
                case 6:
                    item6Image.SetActive(true);
                    break;
                case 7:
                    item3Image.SetActive(true);
                    break;
                default:
                    break;
            }

            inventory.SetActive(false);
            //player.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            string filepath = "/Dialogs/Doll" + dollNumber + "Note.json";

            Debug.Log(filepath);

            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + filepath,
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);

                        switch (dollNumber)
                        {
                            case 1:
                                item8Image.SetActive(false);
                                break;
                            case 2:
                                item9Image.SetActive(false);
                                break;
                            case 3:
                                item4Image.SetActive(false);
                                break;
                            case 4:
                                item5Image.SetActive(false);
                                break;
                            case 5:
                                item7Image.SetActive(false);
                                break;
                            case 6:
                                item6Image.SetActive(false);
                                break;
                            case 7:
                                item3Image.SetActive(false);
                                break;
                            default:
                                break;
                        }

                        inventory.SetActive(true);
                    }));
                }));
        }
        else
        {
            manager.dollSelected = dollNumber;

            switch (dollNumber)
            {
                case 1:
                    item8.SetActive(false);
                    break;
                case 2:
                    item9.SetActive(false);
                    break;
                case 3:
                    item4.SetActive(false);
                    break;
                case 4:
                    item5.SetActive(false);
                    break;
                case 5:
                    item7.SetActive(false);
                    break;
                case 6:
                    item6.SetActive(false);
                    break;
                case 7:
                    item3.SetActive(false);
                    break;
                default:
                    break;
            }

            switch (manager.cribNumber)
            {
                case 1:
                    crib1.changeSprite(dollNumber);
                    crib1.canInteract = true;
                    break;
                case 2:
                    crib2.changeSprite(dollNumber);
                    crib2.canInteract = true;
                    break;
                case 3:
                    crib3.changeSprite(dollNumber);
                    crib3.canInteract = true;
                    break;
                case 4:
                    crib4.changeSprite(dollNumber);
                    crib4.canInteract = true;
                    break;
                case 5:
                    crib5.changeSprite(dollNumber);
                    crib5.canInteract = true;
                    break;
                case 6:
                    crib6.changeSprite(dollNumber);
                    crib6.canInteract = true;
                    break;
                case 7:
                    crib7.changeSprite(dollNumber);
                    crib7.canInteract = true;
                    break;
            }

            inventory.SetActive(false);
            player.canMove = true;
            manager.atCrib = false;
        }
    }

    public void getAntibiotic()
    {
        item2.SetActive(true);
        manager.hasAntibiotic = true;
    }

    public void getLiver()
    {
        item10.SetActive(false);
        item1.SetActive(true);
        manager.hasLiver = true;
        manager.hasScalpel = false;
    }

    public void getScalpel()
    {
        item10.SetActive(true);
        manager.hasScalpel = true;
    }

    public void checkLiver()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();

        string filepath = "/Dialogs/ExamineLiver.json";

        inventory.SetActive(false);
        item1Image.SetActive(true);

        StartCoroutine(FileReader.GetText(
            Application.streamingAssetsPath + filepath,
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    inventory.SetActive(true);
                    item1Image.SetActive(false);
                }));
            }));
    }

    public void checkAntibiotic()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();

        string filepath = "/Dialogs/ExamineAntibiotic.json";

        inventory.SetActive(false);
        item2Image.SetActive(true);

        StartCoroutine(FileReader.GetText(
            Application.streamingAssetsPath + filepath,
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    inventory.SetActive(true);
                    item2Image.SetActive(false);
                }));
            }));
    }

    public void checkScalpel()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();

        string filepath = "/Dialogs/ExamineScalpel.json";

        inventory.SetActive(false);
        item10Image.SetActive(true);

        StartCoroutine(FileReader.GetText(
            Application.streamingAssetsPath + filepath,
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    inventory.SetActive(true);
                    item10Image.SetActive(false);
                }));
            }));
    }

    public void changeBGMVolume(float volume)
    {
        bgm.volume = volume;
    }
}