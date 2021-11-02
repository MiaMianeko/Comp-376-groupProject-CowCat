using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueHallPlayerInput : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    public bool canMove;
    private bool isMovingRight;
    private bool isMovingUp;
    private bool isMovingDown;
    private bool isMoving;
    private bool isEnteringRoom;
    private bool isEntering;

    private bool canChangeScene;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        isMovingRight = false;
        isMovingUp = false;
        isMovingDown = false;
        isMoving = false;
        canMove = false;
        isEnteringRoom = false;
        canChangeScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (vertical != 0.0f)
        {
            horizontal = 0.0f;
        }

        
        Vector3 direction = new Vector3(horizontal, vertical, 0.0f);
        if (!canMove)
        {
            direction=Vector3.zero;
        }
        direction = direction.normalized;
        
        // Translate the game object
        Vector3 delta = direction * speed * Time.deltaTime;
        _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(delta.x, delta.y));


        if (direction != Vector3.zero)
        {
            isMoving = true;
            if (direction.y > 0)
            {
                isMovingUp = true;
                isMovingDown = false;
            }
            else
            {
                if (direction.y < 0)
                {
                    isMovingDown = true;
                    isMovingUp = false;
                }
                else
                {
                    if (direction.x > 0)
                    {
                        isMovingRight = true;
                        _spriteRenderer.flipX = false;
                        isMovingDown = false;
                        isMovingUp = false;
                    }
                    else if (direction.x < 0)
                    {
                        isMovingRight = false;
                        _spriteRenderer.flipX = true;
                        isMovingDown = false;
                        isMovingUp = false;
                    }
                }
            }
        }
        else
        {
            isMoving = false;
            isMovingDown = false;
            isMovingRight = false;
            isMovingUp = false;
        }

        GetComponent<Animator>().SetBool("isMovingHorizontal", isMovingRight);
        GetComponent<Animator>().SetBool("isMovingUp", isMovingUp);
        GetComponent<Animator>().SetBool("isMovingDown", isMovingDown);
        GetComponent<Animator>().SetBool("isMoving", isMoving);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var door = GetComponent<Door060>();
        if (col.tag == "Door"&&door!=null)
        {
            if (door.canInteract)
            {
                canChangeScene = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        var door = GetComponent<Door060>();
        if (col.tag == "Door"&& door!=null)
        {
            canChangeScene = false;
        }
    }
}
