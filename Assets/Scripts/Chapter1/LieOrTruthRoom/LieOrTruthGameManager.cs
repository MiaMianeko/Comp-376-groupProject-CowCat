using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieOrTruthGameManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;
    private UserInput _userInput;
    public int roundNumber = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _userInput = FindObjectOfType<UserInput>();
        _userInput.canMove = false;
        Invoke(nameof(LoadDialog1), 1.0f);
    }


    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1LieOrTruthDialog1.json",
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
}