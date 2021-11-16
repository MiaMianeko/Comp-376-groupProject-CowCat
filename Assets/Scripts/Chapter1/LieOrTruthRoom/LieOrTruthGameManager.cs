using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LieOrTruthGameManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject paintingWallObject;
    [SerializeField] private GameObject selectionBoxGameObject1;
    [SerializeField] private GameObject selectionBoxGameObject2;
    [SerializeField] private GameObject selectionBoxGameObject3;
    [SerializeField] private GameObject backgroundGameObject;

    [SerializeField] private Sprite spriteBackground2;
    [SerializeField] private Sprite spriteBackground3;
    [SerializeField] private Sprite spriteBackground4;

    [SerializeField] private GameObject painting1TriggerGameObject;
    [SerializeField] private GameObject painting2TriggerGameObject;
    [SerializeField] private GameObject painting3TriggerGameObject;
    [SerializeField] private GameObject painting4TriggerGameObject;
    [SerializeField] private GameObject painting5TriggerGameObject;
    [SerializeField] private GameObject badEndingGameObject;

    private Dialog _dialog;
    private UserInput _userInput;
    public int roundNumber = 1;
    private bool isGameOver = false;


    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        Invoke(nameof(LoadDialog1), 1.0f);
        FindObjectOfType<FriendController>().isFacingUp = true;
    }

    private void Update()
    {
        if (isGameOver && Input.anyKey)
        {
           GameOver.ReturnToMainMenu();
        }
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
        CloseSelectionBox1();
        dialogGameObject.SetActive(true);
        if (number == 1)
        {
            // Correct answer
            backgroundGameObject.GetComponent<SpriteRenderer>().sprite = spriteBackground2;
            Destroy(painting1TriggerGameObject);
            roundNumber++;
            FindObjectOfType<LieOrTruthFriend>().roundNumber++;
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
            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog5.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);
                        _userInput.canMove = true;
                        print("Game Over!!!");

                        badEndingGameObject.SetActive(true);
                        Invoke("ChangeGameStatus", 2.5f);
                    }));
                }));
        }
    }

    public void ChangeGameStatus()
    {
        isGameOver = true;
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

    public void SelectLiar2(int number)
    {
        CloseSelectionBox2();
        dialogGameObject.SetActive(true);
        if (number == 3)
        {
            // Correct answer
            backgroundGameObject.GetComponent<SpriteRenderer>().sprite = spriteBackground3;
            Destroy(painting3TriggerGameObject);
            Destroy(painting5TriggerGameObject);
            roundNumber++;
            FindObjectOfType<LieOrTruthFriend>().roundNumber++;
            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog7.json",
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
            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog8.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);
                        _userInput.canMove = true;
                        print("Game Over!!!");

                        badEndingGameObject.SetActive(true);
                        Invoke("ChangeGameStatus", 2.5f);
                    }));
                }));
        }
    }

    public void ShowSelectionBox2()
    {
        selectionBoxGameObject2.SetActive(true);
    }

    public void CloseSelectionBox2()
    {
        _userInput.canMove = true;
        selectionBoxGameObject2.SetActive(false);
    }


    public void SelectLiar3(int number)
    {
        CloseSelectionBox3();
        dialogGameObject.SetActive(true);
        if (number == 3)
        {
            // Correct answer
            backgroundGameObject.GetComponent<SpriteRenderer>().sprite = spriteBackground4;
            Destroy(painting3TriggerGameObject);
            roundNumber++;
            FindObjectOfType<LieOrTruthFriend>().roundNumber++;
            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog7.json",
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
            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog8.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);
                        _userInput.canMove = true;
                        print("Game Over!!!");

                        badEndingGameObject.SetActive(true);
                        Invoke("ChangeGameStatus", 2.5f);
                        
                        
                    }));
                }));
        }
    }

    public void ShowSelectionBox3()
    {
        selectionBoxGameObject3.SetActive(true);
    }

    public void CloseSelectionBox3()
    {
        _userInput.canMove = true;
        selectionBoxGameObject3.SetActive(false);
    }
}