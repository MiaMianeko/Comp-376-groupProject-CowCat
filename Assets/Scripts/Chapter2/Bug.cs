using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bug : MonoBehaviour, IPointerClickHandler
{

    public bool deadBug;
    bool forward = true;
    [SerializeField] private Vector2 endOfForwardMovement;
    [SerializeField] private Vector2 endOfBackwardMovement;
    [SerializeField] private Vector3 forwardMovement;
    [SerializeField] private float rotationSpeed;
    private float speed;
    private float maxRotation;
    private float minRotation;

    private bool positiveRotation;
    Vector3 roty;
    // Start is called before the first frame update
    void Start()
    {
        forwardMovement = forwardMovement.normalized;
        speed = Random.Range(120, 200);
        minRotation = this.gameObject.transform.rotation.z - 0.05f;
        maxRotation = this.gameObject.transform.rotation.z + 0.05f;
        roty = new Vector3(0, 0, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (forward == true)
        {
            if (this.gameObject.transform.position.x > endOfForwardMovement.x || this.gameObject.transform.position.y > endOfForwardMovement.y) forward = false;
            else this.gameObject.transform.position += forwardMovement * Time.deltaTime*speed;
        }
        else if (forward == false)
        {
            if (this.gameObject.transform.position.x < endOfBackwardMovement.x || this.gameObject.transform.position.y < endOfBackwardMovement.y) forward = true;
            else this.gameObject.transform.position += -forwardMovement * Time.deltaTime*speed;
        }
        if (positiveRotation)
        {
            this.gameObject.transform.Rotate(roty, rotationSpeed*Time.deltaTime);
            if (this.gameObject.transform.rotation.z > maxRotation) positiveRotation = false;
        }
        if (!positiveRotation)
        {
            this.gameObject.transform.Rotate(roty, -rotationSpeed * Time.deltaTime);
            if (this.gameObject.transform.rotation.z < minRotation) positiveRotation = true;
        }

    }
    public void OnPointerClick(PointerEventData data)
    {
        deadBug = true;

    }


}
