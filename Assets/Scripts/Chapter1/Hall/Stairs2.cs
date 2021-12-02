using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs2 : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject end;
    void Start()
    {
        end.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            end.SetActive(true);
            Invoke(nameof(LoadScene),1);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Hallway3");
    }
}
