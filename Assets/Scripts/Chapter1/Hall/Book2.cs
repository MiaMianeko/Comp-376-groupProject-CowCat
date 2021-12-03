using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book2 : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject door050;
    [SerializeField] private GameObject notebookpage1;
    [SerializeField] private GameObject notebookpage2;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject notebookpage3;
    [SerializeField] private GameObject shock;
    [SerializeField] private GameObject stairs;
    [SerializeField] private GameObject lockedDoorObject;

    private Dialog _dialog;
    private UserController _userInput;

    void Start()
    {
        notebookpage1.SetActive(false);
        notebookpage2.SetActive(false);
        notebookpage3.SetActive(false);
        door050.GetComponent<BoxCollider2D>().enabled = false;
        shock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F) && !isInteracted)
        {
            notebookpage1.SetActive(true);
            LoadDialogue11();
            canInteract = false;
        }

        if (canInteract && Input.GetKey(KeyCode.F) && isInteracted)
        {
            notebookpage1.SetActive(true);
            canInteract = false;
        }
    }

    public void LoadDialogue11()
    {
        notebookpage1.SetActive(true);
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserController>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog11.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    stairs.GetComponent<BoxCollider2D>().enabled = true;
                }));
            }));
    }

    public void LoadDialogue12()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserController>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog12.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    button.SetActive(true);
                }));
            }));
    }

    public void LoadDialogue13()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserController>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog13.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () => { dialogGameObject.SetActive(false); }));
            }));
    }

    public void TurnPage2()
    {
        notebookpage1.SetActive(false);
        notebookpage2.SetActive(true);
        button.SetActive(false);
        if (!isInteracted)
        {
            Invoke(nameof(LoadDialogue12), 2);
        }
        else
        {
            button.SetActive(true);
        }
    }

    public void TurnPage3()
    {
        notebookpage2.SetActive(false);
        notebookpage3.SetActive(true);
        if (!isInteracted)
        {
            shock.SetActive(true);
            Invoke(nameof(LoadDialogue13), 1);
            isInteracted = true;
            door050.GetComponent<BoxCollider2D>().enabled = true;
            lockedDoorObject.SetActive(false);
        }
    }

    public void CloseNote()
    {
        notebookpage1.SetActive(false);
        notebookpage2.SetActive(false);
        notebookpage3.SetActive(false);
        _userInput.canMove = true;
    }
}