using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToyRoomGameManager : MonoBehaviour
{
    [SerializeField] private GameObject friendGameObject;
    [SerializeField] private GameObject playerGameObject;
    private FriendController _friend;
    private UserInput _player;

    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject selectionBoxObject;
    private Dialog _dialog;
    [SerializeField] private GameObject bearGameObject;
    [SerializeField] private GameObject gameOverGameObject;

    void Start()
    {
        _friend = FindObjectOfType<FriendController>();
        _player = FindObjectOfType<UserInput>();
        Invoke(nameof(LoadDialog1), 1.0f);
        _friend.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    StartCoroutine(FriendMove(() => { LoadDialog2(); }));
                }));
            }));
    }

    public void LoadDialog2()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog2.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _player.canMove = true;
                }));
            }));
    }

    public IEnumerator FriendMove(Action callback)
    {
        _friend.direction = Vector3.up;
        yield return new WaitForSeconds(1.0f);
        _friend.direction = Vector3.zero;
        callback();
    }

    public void SelectBox(int choice)
    {
        selectionBoxObject.SetActive(false);
        if (choice == 5)
        {
            LoadDialog3();
        }
        else
        {
            LoadDialog4();
        }
    }

    public void LoadDialog3()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog4.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _player.canMove = true;
                }));
            }));
    }

    // Select the wrong answer
    void LoadDialog4()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog4.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _player.canMove = true;
                    StartCoroutine(ChooseBearAnimation());
                }));
            }));
    }

    void LoadDialog6()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog6.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    StartCoroutine(GameOverAnimation());
                }));
            }));
    }

    IEnumerator ChooseBearAnimation()
    {
        _player.isControlledBySystem = true;
        _player.direction = Vector3.left;
        yield return new WaitForSeconds(0.3f);
        _player.direction = Vector3.down;
        yield return new WaitForSeconds(0.4f);
        _player.direction = Vector3.zero;
        bearGameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(6.0f);
        LoadDialog6();
    }

    IEnumerator GameOverAnimation()
    {
        gameOverGameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenu");
    }
}