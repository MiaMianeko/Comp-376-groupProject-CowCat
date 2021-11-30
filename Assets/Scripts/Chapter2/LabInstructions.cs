using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabInstructions : Interactable
{
    [SerializeField] GameObject note;

    UserInput player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<UserInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            note.SetActive(true);
            player.canMove = false;
        }
    }

    public void clickCloseButton()
    {
        note.SetActive(false);
        player.canMove = true;
    }
}
