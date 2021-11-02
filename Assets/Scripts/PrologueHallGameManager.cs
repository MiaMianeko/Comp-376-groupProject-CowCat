using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class PrologueHallGameManager : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] GameObject Player;
    
    [SerializeField] private GameObject Dialog;
    private bool _canOpenBag;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject Door060;
    public bool end;
    // Start is called before the first frame update
    public PrologueHallGameManager()
    {
        _canOpenBag = false;
    }
    void Start()
    {
        Player=GameObject.Find("Player");
        Dialog = GameObject.Find("Dialog");
        Door060 = GameObject.Find("Door060");
        end = false;
        _dialog = FindObjectOfType<Dialog>();
        string jsonData1 = File.ReadAllText(Application.dataPath + "/Dialogs/ProloguehallSceneDialog1.json");
        DialogData dialogData1 = JsonUtility.FromJson<DialogData>(jsonData1);
        PrologueHallPlayerInput player = Player.GetComponent<PrologueHallPlayerInput>();
        player.canMove = false;
        Door060 door = Door060.GetComponent<Door060>();
        StartCoroutine(OutputDialog(dialogData1, nameof(moveToClass)));
    }

    // Update is called once per frame
    void Update()
    {
        Door060 door = Door060.GetComponent<Door060>();
        if (door.canInteract)
        {
            if (Input.GetKey(KeyCode.F))
            {
                changeScnen();
            }
        }
    }

    private void changeScnen()
    {
        SceneManager.LoadScene("Scenes/Prologue/ClassroomScene");
    }
    private void moveToClass()
    {
        PrologueHallPlayerInput player = Player.GetComponent<PrologueHallPlayerInput>();
        player.canMove = true;
        Dialog.SetActive(false);
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
}
