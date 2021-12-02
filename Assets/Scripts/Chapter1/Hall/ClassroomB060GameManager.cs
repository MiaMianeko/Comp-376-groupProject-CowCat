using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassroomB060GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject start;
    private UserController _userInput;
    private Dialog _dialog;

    void Start()
    {
        _userInput = FindObjectOfType<UserController>();
        _userInput.canMove = true;
        start.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}