using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomSceneUserInput : MonoBehaviour
{
    private float speed = 4.0f;

    [SerializeField] private GameObject backgroundGameObject;

    public bool canMove;

    private bool isMovingRight;

    private bool isMovingUp;

    private bool isMovingDown;

    private SpriteRenderer _spriteRenderer;

    private bool isMoving;

    public ClassroomSceneUserInput()
    {
        canMove = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isMovingRight = false;
        isMovingUp = false;
        isMovingDown = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (vertical != 0.0f)
        {
            horizontal = 0.0f;
        }
        Vector3 direction = new Vector3(horizontal, vertical, 0.0f);
        direction = direction.normalized;

        if (backgroundGameObject.gameObject.transform.position.y < 5)
        {
            backgroundGameObject.gameObject.transform.position =
                new Vector3(backgroundGameObject.gameObject.transform.position.x, 5,
                    backgroundGameObject.gameObject.transform.position.z);
        }

        if (backgroundGameObject.gameObject.transform.position.y > 16)
        {
            backgroundGameObject.gameObject.transform.position =
                new Vector3(backgroundGameObject.gameObject.transform.position.x, 16,
                    backgroundGameObject.gameObject.transform.position.z);
        }


        // Translate the game object
        Vector3 delta = direction * speed * Time.deltaTime;
        delta.y = 0;
        transform.position += delta;

        // Translate the background object
        delta = direction * speed * Time.deltaTime;
        delta.x = 0;
        backgroundGameObject.gameObject.transform.position -= delta;


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


        GetComponent<Animator>().SetBool("isMovingHorizontal",isMovingRight );
        GetComponent<Animator>().SetBool("isMovingUp",isMovingUp);
        GetComponent<Animator>().SetBool("isMovingDown",isMovingDown );
        GetComponent<Animator>().SetBool("isMoving",isMoving );
    }
}