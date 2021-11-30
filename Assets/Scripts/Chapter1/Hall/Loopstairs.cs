using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loopstairs : Interactable
{
    [SerializeField] private GameObject LevelReloader;

    private UserController _userInput;


    // Start is called before the first frame update
    void Start()
    {
        _userInput = FindObjectOfType<UserController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            _userInput.isFacingDown = false;
            _userInput.isFacingLeft = false;
            _userInput.isFacingRight = false;
            _userInput.isFacingUp = true;
            Reload();
        }
    }

    public void Reload()
    {
        _userInput.canMove = false;

        ActiveLoader();
        Invoke(nameof(ChangerPlayerDirection), 0.5f);
        Invoke(nameof(DisactiveLoader), 1);
    }

    public void ActiveLoader()
    {
        LevelReloader.SetActive(true);
    }

    public void DisactiveLoader()
    {
        LevelReloader.SetActive(false);
        _userInput.canMove = true;
    }

    public void ChangerPlayerDirection()
    {
        _userInput.isFacingDown = true;
        _userInput.isFacingLeft = false;
        _userInput.isFacingRight = false;
        _userInput.isFacingUp = false;
    }
}