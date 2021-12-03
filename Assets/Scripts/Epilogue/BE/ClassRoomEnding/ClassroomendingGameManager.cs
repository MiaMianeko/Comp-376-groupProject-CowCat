using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomendingGameManager : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject openeyes;
    [SerializeField] private GameObject playerOnChair;
    [SerializeField] private GameObject player;
    private UserController _userInput;
     private Dialog _dialog;
    // Start is called before the first frame update
    void Start()
    {
        
        block.SetActive(true);
        openeyes.SetActive(false);
        Invoke(nameof(LoadDialogue1),2);
        player.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadDialogue1()
    {
        
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
       
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/EpilogueClassroomBE1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    block.SetActive(false);
                    openeyes.SetActive(true);
                    Invoke(nameof(LoadDialogue2),2);
                }));
            }));
    }

    public void LoadDialogue2()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/EpilogueClassroomBE2.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    playerOnChair.SetActive(false);
                    player.SetActive(true);
                    _userInput = FindObjectOfType<UserController>();
                    _userInput.canMove = true;
                }));
            }));
    }
    
}
