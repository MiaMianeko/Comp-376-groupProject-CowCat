using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiverExtractionGame : MonoBehaviour
{
    [SerializeField] CutLine round1Game;
    [SerializeField] GameObject round1Object;

    [SerializeField] BugPuzzle round2Game;
    [SerializeField] GameObject round2Object;

    HospitalManager manager;

    public int round;

    InventoryManager inventory;

    private UserInput _userInput;
    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        _userInput = FindObjectOfType<UserInput>();
        manager = FindObjectOfType<HospitalManager>();
        inventory = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {


        if(round == 1 && round1Game.solved)
        {
            round++;
            round1Object.SetActive(false);
            round2Object.SetActive(true);


        }
        else if (round == 2 && round2Game.solved)
        {
            round++;
            round2Object.SetActive(false);
            manager.hasLiver = true;
            _userInput.canMove = true;
            inventory.getLiver();
        }
    }

    public void StartGame()
    {
        round = 1;
        round1Object.SetActive(true);

    }
}
