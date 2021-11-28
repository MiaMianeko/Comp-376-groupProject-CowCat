using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBook : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject photo;
    [SerializeField] private GameObject breaksound;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject button;
    private Dialog _dialog;
    private UserInput _userInput;
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        breaksound.SetActive(false);
        door.GetComponent<BoxCollider2D>().enabled = false;
        photo.SetActive(false);
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKey(KeyCode.F) )
        {
            
            LoadDialogue16();
            canInteract = false;
            
        }
    }

    public void LoadDialogue16()
    {
        _userInput.canMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog16.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    photo.SetActive(true);
                    Invoke(nameof(LoadDialog17),2);
                }));
            }));
    }

    public void LoadDialog17()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog17.json",
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

    public void LoadDialog18()
    {
        
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog18.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _userInput.canMove = true;
                    door.GetComponent<BoxCollider2D>().enabled = true;
                }));
            }));
    }
    public void ClosePhoto()
    {
        photo.SetActive(false);
        button.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        breaksound.SetActive(true);
        Invoke(nameof(LoadDialog18),3);

    }
}
