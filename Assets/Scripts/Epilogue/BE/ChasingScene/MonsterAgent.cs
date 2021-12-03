using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterAgent : MonoBehaviour
{
    [SerializeField] private GameObject gameOverGameObject;
    [SerializeField] private Transform target;
    private NavMeshAgent monsterAgent;
    private bool isChase;
    private bool playerKilled;

    // Start is called before the first frame update
    void Start()
    {

        monsterAgent = GetComponent<NavMeshAgent>();
        monsterAgent.updateRotation = false;
        monsterAgent.updateUpAxis = false;

        isChase = false;
        playerKilled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (isChase)
        {
            monsterAgent.SetDestination(target.transform.position);
        }

        if (gameObject.transform.position.x <= -39)
        {
            Destroy(gameObject);
            FindObjectOfType<ChasingSceneGameManager>().StopBGM();
        }

        if (playerKilled)
        {
            
            
        }
        
    }

    public void MonsterChase()
    {
        isChase = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerKilled = true;
            gameOverGameObject.SetActive(true);
            Invoke(nameof(Reload), 3.0f);
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene("Scenes/Epilogue/BE/ChasingScene");
    }
    
}


