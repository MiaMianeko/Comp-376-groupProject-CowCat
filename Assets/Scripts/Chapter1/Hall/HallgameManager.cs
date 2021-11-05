using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallgameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject PLayer;
    void Start()
    {
        UserInput player = PLayer.GetComponent<UserInput>();
        player.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
