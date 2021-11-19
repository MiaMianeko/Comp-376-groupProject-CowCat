using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutLine : MonoBehaviour
{


    [SerializeField] LineSegment segment1;
    [SerializeField] LineSegment segment2;
    [SerializeField] LineSegment segment3;
    [SerializeField] LineSegment segment4;
    [SerializeField] LineSegment segment5;

    public bool solved;

    // Start is called before the first frame update
    void Start()
    {
        segment1.canBeRed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (segment1.isRed == false)
            segment1.canBeRed = true;
        if (!Input.GetKey(KeyCode.Mouse0)&& solved == false)
        {
            segment1.isRed = false;
            segment2.isRed = false;
            segment3.isRed = false;
            segment4.isRed = false;
            segment5.isRed = false;

            segment2.canBeRed = false;
            segment3.canBeRed = false;
            segment4.canBeRed = false;
            segment5.canBeRed = false;

            segment1.colorValue.color = Color.white;
            segment2.colorValue.color = Color.white;
            segment3.colorValue.color = Color.white;
            segment4.colorValue.color = Color.white;
            segment5.colorValue.color = Color.white;

        }

        if (segment1.isRed) segment2.canBeRed = true;
        if (segment2.isRed) segment3.canBeRed = true;
        if (segment3.isRed) segment4.canBeRed = true;
        if (segment4.isRed) segment5.canBeRed = true;
        if (segment5.isRed) solved = true;


    }
}
