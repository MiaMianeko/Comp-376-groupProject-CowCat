using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private float speed = 10.0f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    public bool canMove;
    public bool canInteract;
    public bool isMove = false;
    public bool isFacingRight = false;
    public bool isFacingLeft = false;
    public bool isFacingUp = false;
    public bool isFacingDown = false;
    private Transform F;

    UserInput()
    {
        isFacingRight = false;
        isFacingDown = false;
        isFacingUp = false;
        isFacingLeft = false;
        isMove = false;
        canMove = false;
        canInteract = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        F = transform.Find("F");
        if (F != null)
            F.GetComponent<SpriteRenderer>().enabled = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        F = transform.Find("F");

        if (vertical != 0.0f)
        {
            horizontal = 0.0f;
        }

        if (F != null)
        {
            if (canInteract)
            {
                F.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                F.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        Vector3 direction = new Vector3(horizontal, vertical, 0.0f);
        direction = direction.normalized;

        // Translate the game object
        Vector3 delta = direction * speed * Time.fixedDeltaTime;
        _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(delta.x, delta.y));
        
        if (direction.x < 0)
        {
            isMove = true;
            isFacingLeft = true;
            isFacingUp = false;
            isFacingDown = false;
            isFacingRight = false;
        }
        else if (direction.x > 0)
        {
            isMove = true;
            isFacingLeft = false;
            isFacingUp = false;
            isFacingDown = false;
            isFacingRight = true;
        }
        else
        {
            isMove = false;
            if (direction.y > 0)
            {
                isMove = true;
                isFacingLeft = false;
                isFacingUp = true;
                isFacingDown = false;
                isFacingRight = false;
            }
            else if (direction.y < 0)
            {
                isMove = true;
                isFacingLeft = false;
                isFacingUp = false;
                isFacingDown = true;
                isFacingRight = false;
            }
            else
            {
                isMove = false;
            }
        }

        GetComponent<Animator>().SetBool("isFacingLeft", isFacingLeft);
        GetComponent<Animator>().SetBool("isFacingDown", isFacingDown);
        GetComponent<Animator>().SetBool("isFacingUp", isFacingUp);
        GetComponent<Animator>().SetBool("isFacingRight", isFacingRight);
        GetComponent<Animator>().SetBool("isMove", isMove);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Interactable"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Interactable"))
        {
            canInteract = false;
        }
    }
}