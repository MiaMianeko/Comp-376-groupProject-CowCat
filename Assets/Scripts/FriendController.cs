using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public bool isMove = false;
    public bool isFacingRight = false;
    public bool isFacingLeft = false;
    public bool isFacingUp = false;
    public bool isFacingDown = false;

    private float speed = 10.0f;

    public Vector3 direction = new Vector3(0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
}