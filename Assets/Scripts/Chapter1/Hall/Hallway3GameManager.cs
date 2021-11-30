using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway3GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject notebook;
    [SerializeField] private GameObject stairs;
    [SerializeField] private GameObject book;
    private UserController _userInput;
    private Dialog _dialog;

    void Start()
    {
        _userInput = FindObjectOfType<UserController>();
        _userInput.canMove = false;
        book.GetComponent<BoxCollider2D>().enabled = false;
        notebook.SetActive(false);
        stairs.GetComponent<BoxCollider2D>().enabled = false;
        Invoke(nameof(LoadDialogue8), 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadDialogue8()
    {
        _userInput.canMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserController>();
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog8.json",
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
}