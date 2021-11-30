using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door050 : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject openTheDoor;
    private Dialog _dialog;
    private UserController _userInput;

    void Start()
    {
        _userInput = FindObjectOfType<UserController>();
        openTheDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            LoadDialogue14();
        }
    }

    public void LoadDialogue14()
    {
        _userInput.canMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserController>();
        _userInput.canMove = false;
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog14.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    openTheDoor.SetActive(true);
                    Invoke(nameof(ChangeScene), 2);
                }));
            }));
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("ClassroomB060");
    }
}