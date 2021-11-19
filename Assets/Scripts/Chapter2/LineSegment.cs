using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LineSegment : MonoBehaviour, IPointerEnterHandler
{

    public bool canBeRed;
    public bool isRed;
    public Image colorValue;

    // Start is called before the first frame update
    void Start()
    {
        colorValue = transform.GetComponent<Image>();
    }

    // Update is called once per frame


    public void OnPointerEnter(PointerEventData data)
    {
        if (canBeRed)
        {
            isRed = true;
            colorValue.color = Color.red;
        }
    }
}
