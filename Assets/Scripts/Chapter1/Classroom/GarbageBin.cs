using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBin : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canInteract;
    private Transform F;
    public GarbageBin()
    {
        canInteract = false;
    }
    void Start()
    {
        F = transform.Find("F");
        F.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            F = transform.Find("F");
            F.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            F = transform.Find("F");
            F.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (canInteract && Input.GetKey(KeyCode.F))
        {
            print("Garbagebin");
            canInteract = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            canInteract = false;
    }
}