using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnding : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject friend;
    [SerializeField] private GameObject blackFlash;
    [SerializeField] private GameObject darkClass;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject block;
    private Dialog _dialog;
    void Start()
    {
        blackFlash.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F))
        {
            canInteract = false;
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(player);
            Invoke(nameof(BadEnding),3);
        }
    }

    public void BadEnding()
    {
        blackFlash.SetActive(true);
        darkClass.SetActive(true);
        friend.SetActive(true);
        friend.GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadDialogue5),2);
        
    }
    public void LoadDialogue5()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
       
        dialogGameObject.SetActive(true);
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/EpilogueClassroomBE5.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    Invoke(nameof(End),2);
                    
                }));
            }));
    }

    public void End()
    {
        Destroy(friend);
        block.SetActive(true);
    }

    public void ChangeScene()
    {
        
    }
}
