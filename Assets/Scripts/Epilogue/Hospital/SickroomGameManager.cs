using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickroomGameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    private UserController _player;

    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<UserController>();
        _player.canMove = false;
      
        Invoke(nameof(LoadDialog1), 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();

        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/TrueEndDialog.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _player.canMove = true;
                }));
                
            }));
    }

  
}