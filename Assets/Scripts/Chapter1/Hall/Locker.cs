using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject stairs;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject shock;
    private UserController _userInput;
    private Dialog _dialog;

    void Start()
    {
        stairs.GetComponent<BoxCollider2D>().enabled = false;
        _userInput = FindObjectOfType<UserController>();
        shock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            shock.SetActive(true);
            Invoke(nameof(LoadDialogue20), 2);
        }
    }

    public void LoadDialogue20()
    {
        _userInput.canMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserController>();
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog20.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _userInput.canMove = true;
                    stairs.GetComponent<BoxCollider2D>().enabled = true;
                }));
            }));
    }
}