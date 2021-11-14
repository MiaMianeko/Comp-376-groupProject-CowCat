using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class LieOrTruthFriend : Interactable
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserInput _userInput;
    public int roundNumber = 1;
    public bool isMove = false;
    public bool isFacingRight =false;
    public bool isFacingLeft = false;
    public bool isFacingUp = false;
    public bool isFacingDown =false;
 
    private float speed = 10.0f;
     
    public Vector3 direction = new Vector3( 0, 0, 0);
    

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _userInput = FindObjectOfType<UserInput>();
        direction = new Vector3(-1, 0, 0);
    }

    void Update()
    {
        
        
        Vector3 delta = direction * speed * Time.fixedDeltaTime;
        _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(delta.x, delta.y));
        
       
            if (direction.x < 0)
            {
                isMove = true;
                isFacingLeft = true;
                isFacingUp = false;
                isFacingDown = false;
                isFacingRight = false;
                
            }
            else if (direction.x > 0)
            {
                isMove = true;
                isFacingLeft = false;
                isFacingUp = false;
                isFacingDown = false;
                isFacingRight = true;
            }
            else
            {
                isMove = false;
                if (direction.y > 0)
                {
                    isMove = true;
                    isFacingLeft = false;
                    isFacingUp = true;
                    isFacingDown = false;
                    isFacingRight = false;
                }

                else if (direction.y < 0)
                {
                    isMove = true;
                    isFacingLeft = false;
                    isFacingUp = false;
                    isFacingDown = true;
                    isFacingRight = false;
                }
                else
                {
                    isMove = false;
                }
            }
           

           

           

            print("isFacingLeft"+ isFacingLeft + isMove);
        
       
        GetComponent<Animator>().SetBool("isFacingLeft", isFacingLeft);
        GetComponent<Animator>().SetBool("isFacingDown", isFacingDown);
        GetComponent<Animator>().SetBool("isFacingUp", isFacingUp);
        GetComponent<Animator>().SetBool("isFacingRight", isFacingRight);
        GetComponent<Animator>().SetBool("isMove", isMove);
      


       
        
        
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;

            _userInput = FindObjectOfType<UserInput>();
            _userInput.canMove = false;

            dialogGameObject.SetActive(true);
            _dialog = FindObjectOfType<Dialog>();

            switch (roundNumber)
            {
                case 1:
                    StartCoroutine(FileReader.GetText(
                        Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog2.json",
                        jsonData =>
                        {
                            DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                            StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                            {
                                dialogGameObject.SetActive(false);
                                _userInput.canMove = true;
                                roundNumber++;
                                FindObjectOfType<LieOrTruthGameManager>().ReleasePaintingBlock();
                            }));
                        }));
                    break;
            }
        }
    }
}