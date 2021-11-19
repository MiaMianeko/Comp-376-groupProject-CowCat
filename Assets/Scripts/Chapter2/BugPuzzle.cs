using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Bug bug1;
    [SerializeField] Bug bug2;
    [SerializeField] Bug bug3;
    [SerializeField] Bug bug4;

    public bool solved;

    bool bug1dead;
    bool bug2dead;
    bool bug3dead;
    bool bug4dead;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bug1dead && bug1.deadBug)
        {
            bug1.gameObject.SetActive(false);
            bug1dead = true;


        }
        if (!bug2dead && bug2.deadBug)
        {
            bug2.gameObject.SetActive(false);
            bug2dead = true;

        }
        if (!bug3dead && bug3.deadBug)
        {
            bug3.gameObject.SetActive(false);
            bug3dead = true;

        }
        if (!bug4dead && bug4.deadBug)
        {
            bug4.gameObject.SetActive(false);
            bug4dead = true;


        }
        if (bug1dead && bug2dead && bug3dead && bug4dead) solved = true;
    }
}
