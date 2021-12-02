using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEGameManager : MonoBehaviour
{
    [SerializeField] private GameObject friendGameObject;
    [SerializeField] private GameObject playerGameObject;
    private FriendController _friend;
    private UserController _player;

    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    [SerializeField] private GameObject gameOverGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _friend = FindObjectOfType<FriendController>();
        _player = FindObjectOfType<UserController>();
        Invoke(nameof(LoadDialog1), 1.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
    

        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "Dialogs/EpilogueBEBEGameManagerDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    //StartCoroutine(FriendMove(() => { LoadDialog2(); }));
                }));
            }));
    }

    /*
    public IEnumerator FriendMove()
    {
        //return 
    }*/
}


