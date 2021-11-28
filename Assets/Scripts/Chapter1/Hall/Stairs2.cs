using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs2 : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            SceneManager.LoadScene("Hallway3");
        }
    }
}
