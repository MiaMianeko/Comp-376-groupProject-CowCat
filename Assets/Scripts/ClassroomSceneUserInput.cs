using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomSceneUserInput : MonoBehaviour
{
    private float speed = 4.0f;

    [SerializeField] private GameObject backgroundGameObject;

    public bool canMove;

    private bool isMoving;

    private SpriteRenderer _spriteRenderer;

    public ClassroomSceneUserInput()
    {
        canMove = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

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
            if (direction.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        GetComponent<Animator>().SetBool("isMoving", isMoving);
    }
}