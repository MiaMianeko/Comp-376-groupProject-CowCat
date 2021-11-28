using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorof060 : Interactable
{
    [SerializeField] private GameObject openTheDoor;
    // Start is called before the first frame update
    void Start()
    {
        openTheDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            openTheDoor.SetActive(true);
            Invoke(nameof(ChangeScene),2);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Hallway4");
    }
}
