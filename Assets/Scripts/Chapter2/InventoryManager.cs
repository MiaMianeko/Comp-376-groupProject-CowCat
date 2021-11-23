using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] UserInput player;
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

    [SerializeField] GameObject dialogGameObject;

    [SerializeField] Crib crib1;
    [SerializeField] Crib crib2;
    [SerializeField] Crib crib3;
    [SerializeField] Crib crib4;
    [SerializeField] Crib crib5;
    [SerializeField] Crib crib6;
    [SerializeField] Crib crib7;

    HospitalManager manager;
    Dialog _dialog;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<HospitalManager>();
        _dialog = FindObjectOfType<Dialog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.canMove && !isOpen && Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(true);
            isOpen = true;
            player.canMove = false;
        }
        else if (isOpen && Input.GetKeyDown(KeyCode.I) && !manager.atCrib) closeInventory();
    }

    public void closeInventory()
    {
        player.canMove = true;
        isOpen = false;
        inventory.SetActive(false);
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
                    break;
                case 2:
                    crib2.changeSprite(dollNumber);
                    break;
                case 3:
                    crib3.changeSprite(dollNumber);
                    break;
                case 4:
                    crib4.changeSprite(dollNumber);
                    break;
                case 5:
                    crib5.changeSprite(dollNumber);
                    break;
                case 6:
                    crib6.changeSprite(dollNumber);
                    break;
                case 7:
                    crib7.changeSprite(dollNumber);
                    break;

            }
            inventory.SetActive(false);
            player.canMove = true;
            manager.atCrib = false;
        }
    }

    public void getAntibiotic()
    {

    }

    public void getLiver()
    {

    }

}
