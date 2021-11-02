using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door060 : MonoBehaviour
{
    public bool canInteract;
    private Transform f;


    // Start is called before the first frame update
    void Start()
    {
        f = transform.Find("F");
        canInteract = false;
        f.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            f.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            f.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            canInteract = true;
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            canInteract = false;
    }
}