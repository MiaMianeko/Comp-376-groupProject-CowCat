using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway2GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject notebook;
    [SerializeField] private GameObject stairs;
    private UserController _userInput;

    void Start()
    {
        _userInput = FindObjectOfType<UserController>();
        notebook.SetActive(false);
        stairs.GetComponent<BoxCollider2D>().enabled = false;
        _userInput.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}