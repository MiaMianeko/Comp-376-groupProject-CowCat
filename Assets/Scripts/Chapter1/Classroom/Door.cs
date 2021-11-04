using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canEnter;
    public Transform F;
    public Door()
    {
        
        canEnter = false;
        
    }
    void Start()
    {
        F = transform.Find("F");
        F.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        F = transform.Find("F");
        if (canEnter)
        {
            F.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            F.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (canEnter && Input.GetKey(KeyCode.F))
        {
            print("cnm");
            canEnter = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            canEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            canEnter = false;
    }
}