using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stairs3 : Interactable
{
    [SerializeField] private GameObject end;
    // Start is called before the first frame update
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
        SceneManager.LoadScene("LiesOrTruthScene");
    }
}
