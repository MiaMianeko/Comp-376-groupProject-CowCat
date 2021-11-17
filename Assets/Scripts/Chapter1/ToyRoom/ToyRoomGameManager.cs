using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRoomGameManager : MonoBehaviour
{
    [SerializeField] private GameObject friendGameObject;
    [SerializeField] private GameObject playerGameObject;
    private FriendController _friend;
    private UserInput _player;

    [SerializeField] private GameObject dialogGameObject;
    private Dialog _dialog;

    void Start()
    {
        _friend = FindObjectOfType<FriendController>();
        _player = FindObjectOfType<UserInput>();
        Invoke(nameof(LoadDialog1), 1.0f);
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    StartCoroutine(FriendMove(() => { print("123"); }));
                }));
            }));
    }

    public IEnumerator FriendMove(Action callback)
    {
        _friend.direction = Vector3.up;
        yield return new WaitForSeconds(0.20f);
        _friend.direction = Vector3.left;
        yield return new WaitForSeconds(0.10f);
        _friend.direction = Vector3.up;
        yield return new WaitForSeconds(0.8f);
        _friend.direction = Vector3.zero;
        callback();
    }
}