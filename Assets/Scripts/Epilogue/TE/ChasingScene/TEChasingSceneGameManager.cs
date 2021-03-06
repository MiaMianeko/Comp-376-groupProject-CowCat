using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEChasingSceneGameManager : MonoBehaviour
{
    [SerializeField] private GameObject friendGameObject;
    [SerializeField] private GameObject playerGameObject;
    private FriendController _friend;
    private UserController _player;

    [SerializeField] private GameObject dialogGameObject;

    private Dialog _dialog;
    // [SerializeField] private GameObject gameOverGameObject;

    [SerializeField] private AudioSource bgmAudio;

    // Start is called before the first frame update
    void Start()
    {
        _friend = FindObjectOfType<FriendController>();
        _player = FindObjectOfType<UserController>();
        Invoke(nameof(LoadDialog1), 1.0f);
        _player.canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();


        StartCoroutine(FileReader.GetText(
            Application.streamingAssetsPath + "/Dialogs/EpilogueTEGameManagerDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    // FindObjectOfType<TEFriendAgent>().FriendMove();
                    _player.canMove = true;
                    FindObjectOfType<TEMonsterAgent>().MonsterChase();
                    //StartCoroutine(FriendMove(() => { LoadDialog2(); }));
                }));
            }));
    }

    public void StopBGM()
    {
        bgmAudio.Stop();
    }
    
    public void LoadDialog2()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        _player.canMove = false;

        StartCoroutine(FileReader.GetText(
            Application.streamingAssetsPath + "/Dialogs/EpilogueTEGameManagerDialog2.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    SceneManager.LoadScene("TEHospitalScene");
                }));
            }));
    }
}