using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway4GameManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogGameObject;
    private  UserInput _userInput;
    private Dialog _dialog;
    
    // Start is called before the first frame update
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        
        _userInput.canMove =false;
        LoadDialogue19();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDialogue19()
    {
        _userInput.canMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog19.json",
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
