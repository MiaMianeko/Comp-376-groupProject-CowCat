using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendAgent : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent friendAgent;

    private Vector3 lastPosition;

    public bool isMove = false;
    public bool isFacingRight = false;
    public bool isFacingLeft = false;
    public bool isFacingUp = false;
    public bool isFacingDown = false;

    private bool isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        friendAgent = GetComponent<NavMeshAgent>();
        friendAgent.updateRotation = false;
        friendAgent.updateUpAxis = false;


        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - lastPosition;
        lastPosition = transform.position;

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


        if (transform.position.x <= -57f)
        {
            if (isFirst)
            {
                FindObjectOfType<ChasingSceneGameManager>().LoadDialog2();
                isFirst = false;
            }
        }
    }

    public void FriendMove()
    {
        friendAgent.SetDestination(target.position);
    }
}