using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject playerGameObject;
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserController _userInput;

    void Start()
    {
        _userInput = FindObjectOfType<UserController>();
        _userInput.canMove = false;
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
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _userInput.canMove = true;
                }));
            }));
    }

    // public void LoadDialog2()
    // {
    //     dialogGameObject.SetActive(true);
    //     StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1HallDialog2.json",
    //         jsonData =>
    //         {
    //             DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
    //             StartCoroutine(_dialog.OutputDialog(dialogData, () =>
    //             {
    //                 dialogGameObject.SetActive(false);
    //                 _userInput.canMove = true;
    //             }));
    //         }));
    // }
}