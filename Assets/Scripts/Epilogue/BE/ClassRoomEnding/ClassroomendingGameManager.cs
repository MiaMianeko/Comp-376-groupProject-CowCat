using System;
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
    [SerializeField] private GameObject friend;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject bgm;
    private FriendController _friendController;
    private UserController _userInput;
     private Dialog _dialog;
    // Start is called before the first frame update
    void Start()
    {
        friend.SetActive(true);
        _friendController = FindObjectOfType<FriendController>();
        _friendController.isFacingRight = true;
        _friendController.isMove = false;
        _friendController.isFacingLeft = false;
        _friendController.isFacingUp = false;
        _friendController.isFacingDown = false;
        block.SetActive(true);
        openeyes.SetActive(false);
        bgm.SetActive(false);
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
                    bgm.SetActive(true);
                    Invoke(nameof(LoadDialogue2),2);
                }));
            }));
    }
    public IEnumerator FriendMoveCenter()
    {
        
        _friendController.direction = Vector3.left;
        yield return new WaitUntil(() => friend.GetComponent<Rigidbody2D>().position.x < -4.96f);
        _friendController.direction = Vector3.up;
        yield return new WaitUntil(() => friend.GetComponent<Rigidbody2D>().position.y > -6.58f);
        _friendController.direction = Vector3.left;
        yield return new WaitUntil(() => friend.GetComponent<Rigidbody2D>().position.x < -7.69f);
        Destroy(friend);
        door.GetComponent<AudioSource>().Play();
        _userInput.canMove = true;
        
        
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
                    StartCoroutine(FriendMoveCenter());
                }));
            }));
    }
    
}
