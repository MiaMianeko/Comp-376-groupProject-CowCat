using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedNotebook : InteractableThing
{
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            FindObjectOfType<ChapterOneClassRoomGameManager>().LoadDialog5();
        }
    }
}
