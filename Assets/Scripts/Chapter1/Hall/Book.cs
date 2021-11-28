using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject stairs;
    [SerializeField] private GameObject notebook;
    private Dialog _dialog;
    private UserInput _userInput;
    
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKey(KeyCode.F) && !isInteracted)
        {

            LoadDialogue4();
            canInteract = false;
            isInteracted = true;


        }

        if (canInteract && Input.GetKey(KeyCode.F) && isInteracted)
        {
            LoadDialog7();
            canInteract = false;
        }
    }

    public void LoadDialogue4()
    {
        _userInput.canMove = false;
        _userInput.direction=Vector3.zero;
        _userInput.isMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog4.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    notebook.SetActive(true);
                    
                    dialogGameObject.SetActive(false);
                    //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    stairs.GetComponent<BoxCollider2D>().enabled = true;
                }));
            }));
    }

    public void LoadDialogue5()
    {
        
        dialogGameObject.SetActive(true);
        
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog5.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    _userInput.canMove = true;
                    dialogGameObject.SetActive(false);
                    
                }));
            }));
    }

    public void LoadDialog7()
    {
        _userInput.canMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog7.json",
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
    
    public void CloseNotebook()
    {
        
        notebook.SetActive(false);
        LoadDialogue5();
        
    }
    
}
