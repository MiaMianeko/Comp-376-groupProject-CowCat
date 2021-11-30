using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Interactable
{
    private FriendController _friendController;
    [SerializeField] private GameObject boardGameObject;
    private UserController _player;

    private void Start()
    {
        _friendController = FindObjectOfType<FriendController>();
        _player = FindObjectOfType<UserController>();
    }

    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            boardGameObject.SetActive(true);
            _friendController.GetComponent<BoxCollider2D>().enabled = true;
            _player.canMove = false;
        }
    }

    public void CloseBoard()
    {
        boardGameObject.SetActive(false);
        _player.canMove = true;
    }
}