using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomArea : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject darkClassroom;
    [SerializeField] private GameObject jumpscare;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject blink;
    [SerializeField] private GameObject bgm;
    private UserController _userController;
    private Dialog _dialog;
    void Start()
    {
        darkClassroom.SetActive(false);
        jumpscare.SetActive(false);
        blink.SetActive(false);
        _userController = FindObjectOfType<UserController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        darkClassroom.SetActive(true);
        jumpscare.SetActive(true);
        bgm.GetComponent<AudioSource>().Pause();
        _userController.canMove = false;
        Invoke(nameof(LoadDialogue3),1);
    }
    
    public void RecoverTheScene()
    {
        
        darkClassroom.SetActive(false);
        bgm.GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadDialogue4),2);

    }

    public void LoadDialogue4()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
       
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/EpilogueClassroomBE4.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _userController.canMove = true;
                    Destroy(gameObject);
                }));
            }));
    }
    public void LoadDialogue3()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
       
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/EpilogueClassroomBE3.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    blink.SetActive(true);
                    Invoke(nameof(RecoverTheScene),0.25f);
                }));
            }));
    }
}
