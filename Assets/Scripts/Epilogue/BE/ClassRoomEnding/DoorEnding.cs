using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnding : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    
    [SerializeField] private GameObject blackFlash;
    [SerializeField] private GameObject darkClass;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject tvSound;
    [SerializeField] private GameObject bgm;
    private Dialog _dialog;
    void Start()
    {
        blackFlash.SetActive(false);
        bgm.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.F)&& !isInteracted)
        {
            isInteracted = true;
            canInteract = false;
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(player);
            Invoke(nameof(BadEnding),3);
            
        }

        if (isInteracted)
        {
            bgm.GetComponent<AudioSource>().volume -= 0.01f;
        }
        
    }

    public void BadEnding()
    {
        blackFlash.SetActive(true);
        Invoke(nameof(End),2);
        
        
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
        Destroy(blackFlash);
        tvSound.GetComponent<AudioSource>().Play();
        block.SetActive(true);
        
    }

    public void ChangeScene()
    {
        
    }
}
