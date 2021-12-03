using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendObjectEpilogue : Interactable
{
    RevelationsManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<RevelationsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            manager.chooseBadEnding();
        }
    }
}
