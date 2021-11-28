using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassroomB060GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    private UserInput _userInput;
    private Dialog _dialog;
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
   
}
