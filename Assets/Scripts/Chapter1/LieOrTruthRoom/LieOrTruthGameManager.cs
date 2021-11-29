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
    [SerializeField] private Sprite spriteBackground5;

    [SerializeField] private GameObject painting1TriggerGameObject;
    [SerializeField] private GameObject painting2TriggerGameObject;
    [SerializeField] private GameObject painting3TriggerGameObject;
    [SerializeField] private GameObject painting4TriggerGameObject;
    [SerializeField] private GameObject painting5TriggerGameObject;
    [SerializeField] private GameObject badEndingGameObject;

    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject friendGameObject;

    private Dialog _dialog;
    private UserInput _userInput;
    private FriendController _friendController;
    public int roundNumber = 1;
    private bool isGameOver = false;


    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _friendController = FindObjectOfType<FriendController>();
        _userInput.canMove = false;
        Invoke(nameof(LoadDialog1), 1.0f);
        FindObjectOfType<FriendController>().isFacingUp = true;
    }

    private void Update()
    {
        if (isGameOver && Input.anyKey)
        {
            SceneManager.LoadScene("MainMenu");
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
                        backgroundGameObject.GetComponent<SpriteRenderer>().sprite = spriteBackground3;
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
        Destroy(painting2TriggerGameObject);
        Destroy(painting4TriggerGameObject);
        if (number == 2)
        {
            // Left
            backgroundGameObject.GetComponent<SpriteRenderer>().sprite = spriteBackground4;
            StartCoroutine(FileReader.GetText(
                Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog10.json",
                jsonData =>
                {
                    DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                    StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                    {
                        dialogGameObject.SetActive(false);
                        _userInput.canMove = true;
                        StartCoroutine(MoveLeft());
                    }));
                }));
             }
             else
             {
                 backgroundGameObject.GetComponent<SpriteRenderer>().sprite = spriteBackground5;
                 // Right
                 StartCoroutine(FileReader.GetText(
                     Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog10.json",
                     jsonData =>
                     {
                         DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                         StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                         {
                             dialogGameObject.SetActive(false);
                             _userInput.canMove = true;

                             StartCoroutine(MoveRight());
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

    public IEnumerator MoveLeft()
    {
        _userInput.isControlledBySystem = true;
        _userInput.direction = Vector3.up;
        yield return new WaitUntil(() => { return playerGameObject.GetComponent<Rigidbody2D>().position.y > -0.1f; });
        _userInput.direction = Vector3.left;
        yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.x < -9.7f);
        _userInput.direction = Vector3.up;
        yield return new WaitForSeconds(0.5f);
        _userInput.direction = Vector3.zero;


        _friendController.direction = Vector3.left;
        yield return new WaitUntil(() => friendGameObject.GetComponent<Rigidbody2D>().position.x < -7.00f);
        _friendController.direction = Vector3.up;
        yield return new WaitForSeconds(0.5f);
        _friendController.direction = Vector3.zero;
        SceneManager.LoadScene("ToyRoomScene");
    }

    public IEnumerator MoveRight()
    {
        
        _userInput.isControlledBySystem = true;
        _userInput.direction = Vector3.up;
        yield return new WaitUntil(() => { return playerGameObject.GetComponent<Rigidbody2D>().position.y > -0.1f; });
        _userInput.direction = Vector3.right;
        yield return new WaitUntil(() => { return playerGameObject.GetComponent<Rigidbody2D>().position.x > 0.1f; });
        _userInput.direction = Vector3.up;
        yield return new WaitForSeconds(0.5f);
        _userInput.direction = Vector3.zero;

        _friendController.direction = Vector3.right;
        yield return new WaitUntil(() => { return friendGameObject.GetComponent<Rigidbody2D>().position.x > -2.59f; });
        _friendController.direction = Vector3.up;
        yield return new WaitForSeconds(0.5f);
        _friendController.direction = Vector3.zero;
        SceneManager.LoadScene("ToyRoomScene");
    }
}