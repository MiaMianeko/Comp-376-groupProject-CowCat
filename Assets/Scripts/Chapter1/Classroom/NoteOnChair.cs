using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteOnChair : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canInteract;
    [SerializeField] public GameObject Note4;
    
    public NoteOnChair()
    {
        canInteract = false;
    }
    void Start()
    {
        Note4.SetActive(false);
    }

    public void CloseNote()
    {
        Note4.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            Note4.SetActive(true);
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
        if(col.tag=="Player")
            canInteract = false;
    }
}
