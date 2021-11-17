using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] UserInput player;
    bool isOpen;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;
    [SerializeField] GameObject item4;
    [SerializeField] GameObject item5;
    [SerializeField] GameObject item6;
    [SerializeField] GameObject item7;
    [SerializeField] GameObject item8;
    [SerializeField] GameObject item9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.canMove && !isOpen && Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(true);
            isOpen = true;
            player.canMove = false;
        }
        else if (isOpen && Input.GetKeyDown(KeyCode.I)) closeInventory();
    }

    public void closeInventory()
    {
        player.canMove = true;
        isOpen = false;
        inventory.SetActive(false);
    }
}
