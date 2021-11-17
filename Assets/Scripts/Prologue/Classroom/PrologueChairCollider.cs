using System;
using System.Collections.Generic;
using UnityEngine;

public class PrologueChairCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<PrologueClassroomSceneManager>().ShowDialog2();
    }
}