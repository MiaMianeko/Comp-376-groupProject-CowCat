using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BirksPlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canMove;

    public bool isTalked;
    public bool isInteract;
    public bool canTakePicture;
    private GameObject F2;
    private GameObject F1;
    public bool canTalk;
    public float speed = 0.5f;
    private SpriteRenderer spriteRenderer;
    private bool isMoving;
    public bool gotPic;

    void Start()
    {
        canTakePicture = false;
        canMove = false;
        isTalked = false;
        canTalk = false;
        gotPic = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        isInteract = false;
        F2 = GameObject.Find("F2");
        F1 = GameObject.Find("F1");
        F2.SetActive(false);
        F1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
        if (isInteract)
        {
            canMove = false;
        }

        if (canTalk || canTakePicture)
        {
            if (Input.GetKey(KeyCode.F))
            {
                isInteract = true;
            }
        }


        if (!canMove)
        {
            direction = Vector3.zero;
        }

        if (direction != Vector3.zero)
        {
            isMoving = true;
            isInteract = false;
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            isMoving = false;
        }

        direction = direction.normalized;

        transform.position += direction * speed * Time.deltaTime;

        GetComponent<Animator>().SetBool("isMoving", isMoving);

         GetComponent<Animator>().SetBool("Interact", isInteract);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        canMove = false;
        if (col.tag == "Camera")
        {
            canTakePicture = true;
        }

        if (col.tag == "Sectary")
        {
            canTalk = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Sectary")
        {
            //canTalk = true;  
            F1.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                isInteract = true;
            }
        }

        if (col.tag == "Camera")
        {
            F2.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                isInteract = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Camera")
        {
            F2.SetActive(false);
            canTakePicture = false;
        }

        if (col.tag == "Sectary")
        {
            F1.SetActive(false);
            canTalk = false;
        }
    }
}