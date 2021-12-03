using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SickRoomDoor : Interactable
{
    [SerializeField] private GameObject finalImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            finalImage.SetActive(true);
            StartCoroutine(LoadStaffScene());
        }
    }

    public IEnumerator LoadStaffScene()
    {
        yield return new WaitUntil((() => Input.GetButtonDown("Skip")));
        SceneManager.LoadScene("Staff");
    }
    
}
