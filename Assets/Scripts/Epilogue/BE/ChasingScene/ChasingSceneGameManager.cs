using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChasingSceneGameManager : MonoBehaviour
{
    [SerializeField] private GameObject friendGameObject;
    [SerializeField] private GameObject playerGameObject;
    private FriendController _friend;
    private UserController _player;

    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private Text talk;
    
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
        if (playerGameObject.transform.position.x >=  15f && playerGameObject.transform.position.x < 38f)
        {
            talk.GetComponent<Text>().text = "IS SOMETHING CHASING US?";
        }
        else if(playerGameObject.transform.position.x >= -10f && playerGameObject.transform.position.x < 15f)
        {
            talk.GetComponent<Text>().text = "DID I FORGET ANYTHING?";
        }
        else if(playerGameObject.transform.position.x >= -35f && playerGameObject.transform.position.x < -10f)
        {
            talk.GetComponent<Text>().text = "WHY ARE WE RUNNING?";
        }
        else if(playerGameObject.transform.position.x < -35f)
        {
            talk.GetComponent<Text>().text = "WE ARE GONNA BE LATE TO THE CLASS!";
        }
       
            
    
        

    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();


        StartCoroutine(FileReader.GetText(
            Application.streamingAssetsPath + "/Dialogs/EpilogueBEBEGameManagerDialog1.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    FindObjectOfType<FriendAgent>().FriendMove();
                    _player.canMove = true;
                    FindObjectOfType<MonsterAgent>().MonsterChase();
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


        StartCoroutine(FileReader.GetText(
            Application.streamingAssetsPath + "/Dialogs/EpilogueBEBEGameManagerDialog2.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    SceneManager.LoadScene("ClassroomEnding");
                }));
            }));
    }
}