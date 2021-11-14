using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieOrTruthGameManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject paintingWallObject;
    [SerializeField] private GameObject selectionBoxGameObject1;
    [SerializeField] private GameObject backgroundGameObject;

    [SerializeField] private Sprite spriteBackground2;
    [SerializeField] private Sprite spriteBackground3;
    [SerializeField] private Sprite spriteBackground4;

    [SerializeField] private GameObject painting1TriggerGameObject;
    [SerializeField] private GameObject painting2TriggerGameObject;
    [SerializeField] private GameObject painting3TriggerGameObject;
    [SerializeField] private GameObject painting4TriggerGameObject;
    [SerializeField] private GameObject painting5TriggerGameObject;

    private Dialog _dialog;
    private UserInput _userInput;
    public int roundNumber = 1;


    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        Invoke(nameof(LoadDialog1), 1.0f);
        FindObjectOfType<FriendController>().isFacingUp = true;
    }


    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog1.json",
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

    public void ReleasePaintingBlock()
    {
        Destroy(paintingWallObject);
    }

    public void SelectLiar1(int number)
    {
        if (number == 1)
        {
            // Correct answer
            CloseSelectionBox1();
            dialogGameObject.SetActive(true);
            backgroundGameObject.GetComponent<SpriteRenderer>().sprite = spriteBackground2;
            Destroy(painting1TriggerGameObject);
            roundNumber++;
            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog4.json",
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
        else
        {
            // Wrong answer
            print("you are wrong");
        }
    }

    public void ShowSelectionBox1()
    {
        selectionBoxGameObject1.SetActive(true);
    }

    public void CloseSelectionBox1()
    {
        _userInput.canMove = true;
        selectionBoxGameObject1.SetActive(false);
    }
}