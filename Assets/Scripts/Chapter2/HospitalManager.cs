using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogGameObject;
    private UserInput _userInput;
    private Dialog _dialog;
    public bool dollsPickedUp;
    public bool dollPuzzleSolved;
    public bool hasAntibiotic;
    public bool knowsHowToCure;
    public bool hasLiver;
    public InventoryManager inventory;

    public bool hasAntibiotics;

    public bool atCrib;
    public int cribNumber;

    public int dollSelected;
    

    // Start is called before the first frame update
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        Invoke(nameof(LoadDialog1), 1.0f);
        inventory = FindObjectOfType<InventoryManager>();
        cribNumber = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/HospitalEnter.json",
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

    public void pickUpDolls()
    {
        dollsPickedUp = true;
        inventory.pickUpAllDolls();
    }
}
