using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableThing : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canInteract;

    public InteractableThing()
    {
        canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            canInteract = false;
    }
}