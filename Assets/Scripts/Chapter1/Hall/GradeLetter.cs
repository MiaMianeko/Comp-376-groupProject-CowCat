using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeLetter : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;

    private Dialog _dialog;
    private UserController _userInput;

    void Start()
    {
        _userInput = FindObjectOfType<UserController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            LoadDialog6();
            canInteract = false;
        }
    }

    public void LoadDialog6()
    {
        _userInput.canMove = false;
        _userInput.direction = Vector3.zero;
        _userInput.isMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserController>();
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog6.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _userInput.canMove = true;
                    //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    canInteract = true;
                }));
            }));
    }
}