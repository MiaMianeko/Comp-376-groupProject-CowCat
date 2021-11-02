using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class BirksSenceManager: MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] GameObject Player;
    [SerializeField] private GameObject flash;
    [SerializeField] private GameObject Dialog;
    private bool _canOpenBag;
    [SerializeField] private GameObject dialogGameObject;

    public bool end;
    //[SerializeField] private GameObject bagGameObject;
    public BirksSenceManager()
    {
        _canOpenBag = false;
    }

    void Start()
    {
        //flash=GameObject.Find("Flash");
        //flash.SetActive(false);
        // Initialize the member variables
        Player=GameObject.Find("Player");
        Dialog = GameObject.Find("Dialog");
        end = false;
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.dataPath + "/Dialogs/BirksScenedialog1.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);
        BirksPlayerInput player = Player.GetComponent<BirksPlayerInput>();
        player.canMove = false;
        // Start Play the first Dialog
        
        StartCoroutine(OutputDialog(dialogData1, nameof(moveToSectary)));
        
    }


    // Update is calsled once per frame
    void Update()
    {
        BirksPlayerInput player = Player.GetComponent<BirksPlayerInput>();
        if (player.canTalk && player.isIteract)
        {
            
            Dialog.SetActive(true);
            string jsonData2 = File.ReadAllText(Application.dataPath + "/Dialogs/BirksScenedialog2.json");
            DialogData dialogData2 = JsonUtility.FromJson<DialogData>(jsonData2);
            player.canTalk = false;
            StartCoroutine(OutputDialog(dialogData2, nameof(moveToCamera)));
            

        }
       // print(player.isIteract);
        if (player.canTakePicture && player.isIteract)
        {
            Dialog.SetActive(true);
            string jsonData3 = File.ReadAllText(Application.dataPath + "/Dialogs/BirksScenedialog3.json");
            DialogData dialogData3 = JsonUtility.FromJson<DialogData>(jsonData3);
            StartCoroutine(OutputDialog(dialogData3,nameof(takePictures) ));
            
            
            player.canTakePicture = false;
        }

        if (player.isTalked && player.gotPic&&!end)
        {
            Dialog.SetActive(true);
            string jsonData4 = File.ReadAllText(Application.dataPath + "/Dialogs/BirksScenedialog4.json");
            DialogData dialogData4 = JsonUtility.FromJson<DialogData>(jsonData4);
            StartCoroutine(OutputDialog(dialogData4, nameof(changeScene)));
            end = true;
        }
        
    }


    void takePictures()
    {
        Dialog.SetActive(false);
        BirksPlayerInput player = Player.GetComponent<BirksPlayerInput>();
        flash.SetActive(true);
        player.gotPic = true;
        
        print("take pic");
    }
    void changeScene()
    {
        Dialog.SetActive(false);
        //SceneManager.LoadScene("Scenes/Prologue/");
        BirksPlayerInput player = Player.GetComponent<BirksPlayerInput>();
        player.isIteract = false;
        SceneManager.LoadScene("Scenes/Prologue/PrologueHallScene");
    }
    void moveToSectary()
    {
        
        Dialog.SetActive(false);
        BirksPlayerInput player = Player.GetComponent<BirksPlayerInput>();
        player.canMove = true;
        print("move to sc");

    }

    
    void moveToCamera()
    {
        Dialog.SetActive(false);
        BirksPlayerInput player = Player.GetComponent<BirksPlayerInput>();
        player.canMove = true;
        player.isTalked = true;
        print("move to camera");
        player.isIteract = false;
    }
    private IEnumerator OutputDialog(DialogData dialogData, string callbackFunctionName)
    {
   
        foreach (var jsonDialogData in dialogData.data)
        {
            print(_dialog);
            _dialog.SetSpeaker(jsonDialogData.speaker);
            _dialog.ClearText();
            _dialog.ShowDialog(jsonDialogData.content);
            yield return new WaitForSeconds(jsonDialogData.duration);
        }

        Invoke(callbackFunctionName, 0);
    }

    private void OpenBagAsync()
    {
        _canOpenBag = true;
    }

   
}