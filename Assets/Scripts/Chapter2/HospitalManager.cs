using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogGameObject;
    private UserInput _userInput;
    private Dialog _dialog;
    public bool dollsPickedUp;
    public bool dollsSpawned;
    public bool dollPuzzleSolved;
    public bool hasAntibiotic;
    public bool hasScalpel;
    public bool hasLiver;
    public bool machinePuzzleSolved;
    public InventoryManager inventory;



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

    public void endChapter()
    {

        /*
        END CHAPTER FUNCTION GOES BRR!!
        1. Overlay of quick animation showing bottle falling and breaking (multiframes, like story book).
        2. Player saying she remembers now... This all happened before...
        3. Friend appears from shadows to taunt player, light flickers as more friends spawn with bloody faces.
        4. Light flickers to black as final scene is loaded

        */
    }
}
