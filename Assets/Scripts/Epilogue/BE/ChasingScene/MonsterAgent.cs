using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAgent : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent monsterAgent;
    private bool isChase;


    // Start is called before the first frame update
    void Start()
    {

        monsterAgent = GetComponent<NavMeshAgent>();
        monsterAgent.updateRotation = false;
        monsterAgent.updateUpAxis = false;

        isChase = false;



    }

    // Update is called once per frame
    void Update()
    {
        if (isChase)
        {
            monsterAgent.SetDestination(target.transform.position);
        }
 

    }

    public void MonsterChase()
    {
        isChase = true;
    }
}


