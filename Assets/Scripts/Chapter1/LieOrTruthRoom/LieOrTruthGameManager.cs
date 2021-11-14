using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieOrTruthGameManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject paintingWallObject;
    [SerializeField] private GameObject selectionBoxGameObject1;

    private Dialog _dialog;
    private UserInput _userInput;
    public int roundNumber = 1;


    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        Invoke(nameof(LoadDialog1), 1.0f);
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
            print(number);
        }
        else
        {
            // Wrong answer
            print("you are wrong");
        }
    }

    public void ShowSelectionBox()
    {
        selectionBoxGameObject1.SetActive(true);
    }
}