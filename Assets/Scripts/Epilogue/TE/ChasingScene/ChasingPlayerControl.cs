
using System;
using UnityEngine;

public class ChasingPlayerControl: MonoBehaviour
{
    private bool isFirst = true;
    private void Update()
    {
        if (transform.position.x <= -43f)
        {
            if (isFirst)
            {
                FindObjectOfType<TEChasingSceneGameManager>().LoadDialog2();
                isFirst = false;
            }
        }
    }
}