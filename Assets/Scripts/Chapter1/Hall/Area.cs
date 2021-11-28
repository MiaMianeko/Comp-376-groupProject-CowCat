using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject stairs;
    private Dialog _dialog;
    private UserInput _userInput;
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        
        stairs.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadDialogue3()
    {
        
        _userInput.canMove = false;
        _userInput.direction=Vector3.zero;
        _userInput.isMove = false;
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _userInput = FindObjectOfType<UserInput>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog3.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    
                    dialogGameObject.SetActive(false);
                    _userInput.canMove = true;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }));
            }));
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        LoadDialogue3();
    }
}