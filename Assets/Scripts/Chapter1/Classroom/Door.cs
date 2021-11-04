using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject pinPadGameObject;

    // Start is called before the first frame update
    public bool canEnter;
    public Transform F;
    private bool isFirst;


    public Door()
    {
        canEnter = false;
        isFirst = true;
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
            canEnter = false;
            if (isFirst)
            {
                isFirst = false;
                FindObjectOfType<ChapterOneClassRoomGameManager>().LoadDialog2();
                Invoke(nameof(ShowPinPad), 5.1f);
            }
            else
            {
                ShowPinPad();
            }
        }
    }

    private void ShowPinPad()
    {
        FindObjectOfType<UserInput>().canMove = false;
        pinPadGameObject.SetActive(true);
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