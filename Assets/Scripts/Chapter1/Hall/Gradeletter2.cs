using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gradeletter2 : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject bookGameObject;
    private Dialog _dialog;
    private UserInput _userInput;
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKey(KeyCode.F) )
        {
            
            LoadDialog10();
            canInteract = false;
        }

        
    }

    public void LoadDialog10()
    {
        _userInput.canMove = false;
        _userInput.direction=Vector3.zero;
        _userInput.isMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog10.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _userInput.canMove = true;
                    //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    bookGameObject.GetComponent<BoxCollider2D>().enabled = true;
                    
                }));
            }));
    }
}
