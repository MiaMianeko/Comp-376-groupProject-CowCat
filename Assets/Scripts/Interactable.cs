using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for all scene elements with which the player can interact.
public class Interactable : MonoBehaviour
{
    //In case it gives an Item or in case it needs an item to resolve a riddle
    [SerializeField] Item receivedFromInteraction;
    [SerializeField] Item solutionToRiddle;

    //Not sure if dialog or dialogdata, dialog provided by "investigating" the object
    [SerializeField] DialogData investigateDialog;
    [SerializeField] DialogData correctItemGivenDialog;
    [SerializeField] DialogData wrongItemGivenDialog;

    //If the interactable object is still active 
    bool active;


    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when player "talks" to the game object
    public void onInteract()
    {

    }

    //when player gives the correct item to the game object
    public void onGiveCorrectItem()
    {

    }
    //when player gives the wrong item to the game object
    public void onGiveWrongItem()
    {

    }

}
