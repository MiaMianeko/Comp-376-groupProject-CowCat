using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirksPlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canMove;
    public bool isTalked;
    public bool isFacingUp;
    public bool canTakePicture;
    private GameObject F2;
    private GameObject F1;
    public bool canTalk;
    public float speed = 0.5f;
    private bool isMove;
    public bool gotPic;
    private bool isFacingRight;

    void Start()
    {
        canTakePicture = false;
        canMove = false;
        isTalked = false;
        canTalk = false;
        gotPic = false;
        F2 = GameObject.Find("F2");
        F1 = GameObject.Find("F1");
        F2.SetActive(false);
        F1.SetActive(false);
        isFacingUp = false;
        isFacingRight = true;
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
        if (isFacingUp)
        {
            canMove = false;
        }

        if (canTalk || canTakePicture)
        {
            if (Input.GetKey(KeyCode.F))
            {
                isFacingRight = false;
                isFacingUp = true;
            }
        }

        if (!canMove)
        {
            direction = Vector3.zero;
        }

        direction = direction.normalized;
        transform.position += direction * speed * Time.deltaTime;


        if (direction.x < 0)
        {
            isMove = true;
            isFacingUp = false;
            isFacingRight = false;
        }
        else if (direction.x > 0)
        {
            isMove = true;
            isFacingUp = false;
            isFacingRight = true;
        }
        else
        {
            isMove = false;
            if (direction.y > 0)
            {
                isMove = true;
                isFacingUp = true;
                isFacingRight = false;
            }
            else if (direction.y < 0)
            {
                isMove = true;
                isFacingUp = false;
                isFacingRight = false;
            }
            else
            {
                isMove = false;
            }
        }

        GetComponent<Animator>().SetBool("isMove", isMove);
        GetComponent<Animator>().SetBool("isFacingRight", isFacingRight);
        GetComponent<Animator>().SetBool("isFacingUp", isFacingUp);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        canMove = false;

        if (col.tag == "Camera")
        {
            isMove = false;
            canTakePicture = true;
        }

        if (col.tag == "Sectary")
        {
            isMove = false;
            canTalk = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Sectary")
        {
            F1.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                isFacingUp = true;
                isFacingRight = false;
            }
        }

        if (col.tag == "Camera")
        {
            F2.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                isFacingUp = true;
                isFacingRight = false;
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