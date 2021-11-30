using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ToyRoomGameManager : MonoBehaviour
{
    [SerializeField] private GameObject friendGameObject;
    [SerializeField] private GameObject playerGameObject;
    private FriendController _friend;
    private UserInput _player;

    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject selectionBoxObject;
    private Dialog _dialog;
    [SerializeField] private GameObject bearGameObject;
    [SerializeField] private GameObject gameOverGameObject;

    int chosenBear = 0;

    public ToyRoomProgress progress;

    bool going;
    float timeToGo;


    void Start()
    {
        _friend = FindObjectOfType<FriendController>();
        _player = FindObjectOfType<UserInput>();
        Invoke(nameof(LoadDialog1), 1.0f);
        _friend.GetComponent<BoxCollider2D>().enabled = false;
        try
        {
            progress = ToyRoomProgress.CreateFromJSON(
                File.ReadAllText(Application.streamingAssetsPath + "/ProgressData.json"));
        }
        catch (Exception)
        {
            progress = new ToyRoomProgress();
        }
    }

    private void Update()
    {
        if (going && Time.time > timeToGo + 1.5f) endScene();
        if (going && Time.time > timeToGo + 0.4f)
        {
            _friend.GetComponent<SpriteRenderer>().enabled = false;
            _player.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void LoadDialog1()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        string filename;
        if (progress.beenThereBefore) filename = "/Dialogs/Chapter1ToyRoomDialog1Alt.json";
        else filename = "/Dialogs/Chapter1ToyRoomDialog1.json";

        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + filename,
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    StartCoroutine(FriendMove(() => { LoadDialog2(); }));
                }));
            }));
    }

    public void LoadDialog2()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        string filename;
        if (progress.beenThereBefore) filename = "/Dialogs/Chapter1ToyRoomDialog2Alt.json";
        else filename = "/Dialogs/Chapter1ToyRoomDialog2.json";

        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + filename,
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _player.canMove = true;
                }));
            }));
    }

    public IEnumerator FriendMove(Action callback)
    {
        _friend.direction = Vector3.up;
        yield return new WaitForSeconds(1.0f);
        _friend.direction = Vector3.left;
        yield return new WaitForSeconds(0.2f);
        _friend.direction = Vector3.zero;
        _friend.isFacingLeft = false;
        _friend.isFacingRight = true;
        callback();
    }

    public IEnumerator FriendMoveCenter(Action callback)
    {
        _friend.direction = Vector3.down;
        yield return new WaitUntil(() => friendGameObject.GetComponent<Rigidbody2D>().position.y < 2);
        _friend.direction = Vector3.right;
        yield return new WaitUntil(() => friendGameObject.GetComponent<Rigidbody2D>().position.x > 0);
        _friend.direction = Vector3.zero;
        _friend.isFacingLeft = true;
        _friend.isFacingRight = false;
        callback();
    }

    public void SelectBox(int choice)
    {
        chosenBear = choice;
        selectionBoxObject.SetActive(false);
        if (choice == 5)
        {
            LoadDialog3();
        }
        else if (choice == 0)
        {
            _player.canMove = true;
        }
        else
        {
            LoadDialog4();
        }
    }

    // Selected Correct Answer
    public void LoadDialog3()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog4.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _player.canMove = true;
                    StartCoroutine(ChooseBearAnimationCorrect());
                }));
            }));
    }

    // Selected the wrong answer
    void LoadDialog4()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog4.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);
                    _player.canMove = true;
                    StartCoroutine(ChooseBearAnimationIncorrect());
                }));
            }));
    }

    void LoadDialog5()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog5.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);

                    going = true;
                    timeToGo = Time.time;
                    bearGameObject.GetComponent<Animator>().SetBool("BRCorrect", true);
                }));
            }));
    }

    void LoadDialog6()
    {
        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        StartCoroutine(FileReader.GetText(Application.streamingAssetsPath + "/Dialogs/Chapter1ToyRoomDialog6.json",
            jsonData =>
            {
                DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
                StartCoroutine(_dialog.OutputDialog(dialogData, () =>
                {
                    dialogGameObject.SetActive(false);

                    progress.beenThereBefore = true;
                    string jsonProgress = progress.SaveToString();
                    File.WriteAllText(Application.streamingAssetsPath + "/ProgressData.json", jsonProgress);
                    StartCoroutine(GameOverAnimation());
                }));
            }));
    }

    IEnumerator ChooseBearAnimationCorrect()
    {
        float x, y;
        x = -4.06f;
        y = 0.24f;
        _player.isControlledBySystem = true;
        if (playerGameObject.GetComponent<Rigidbody2D>().position.x > x)
        {
            _player.direction = Vector3.left;

            yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.x < x);
        }
        else
        {
            _player.direction = Vector3.right;

            yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.x > x);
        }

        if (playerGameObject.GetComponent<Rigidbody2D>().position.y > y)
        {
            _player.direction = Vector3.down;
            yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.y < y);
        }
        else
        {
            _player.direction = Vector3.up;
            yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.y > y);
        }

        _player.direction = Vector3.zero;
        bearGameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(6.0f);
        afterCorrectSelection();
    }

    private void afterCorrectSelection()
    {
        progress.beenThereBefore = false;
        string jsonProgress = progress.SaveToString();
        try
        {
            File.WriteAllText(Application.streamingAssetsPath + "/ProgressData.json", jsonProgress);
        }
        catch (Exception e)
        {
        }

        StartCoroutine(FriendMoveCenter(() => { LoadDialog5(); }));
    }

    IEnumerator ChooseBearAnimationIncorrect()
    {
        float x = 0.0f, y = 0.0f;
        _player.isControlledBySystem = true;
        switch (chosenBear)
        {
            case 1:
                x = 0.21f;
                y = 6.54f;
                break;
            case 2:
                x = -2.52f;
                y = 6.21f;
                break;
            case 3:
                x = -4.17f;
                y = 4.79f;
                break;
            case 4:
                x = -4.57f;
                y = 2.35f;
                break;
            case 6:
                x = -1.95f;
                y = -1.39f;
                break;
            case 7:
                x = 0f;
                y = -2f;
                break;
            case 8:
                x = 2.48f;
                y = -1.47f;
                break;
            case 9:
                x = 4.08f;
                y = -0.12f;
                break;
            case 10:
                x = 4.7f;
                y = -2.12f;
                break;
            case 11:
                x = 4.17f;
                y = 4.27f;
                break;
            case 12:
                x = 3.0f;
                y = 6.0f;
                break;
        }

        if (playerGameObject.GetComponent<Rigidbody2D>().position.x > x)
        {
            _player.direction = Vector3.left;
            yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.x < x);
        }
        else
        {
            _player.direction = Vector3.right;
            yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.x > x);
        }

        if (playerGameObject.GetComponent<Rigidbody2D>().position.y > y)
        {
            _player.direction = Vector3.down;
            yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.y < y);
        }
        else
        {
            _player.direction = Vector3.up;
            yield return new WaitUntil(() => playerGameObject.GetComponent<Rigidbody2D>().position.y > y);
        }

        _player.direction = Vector3.zero;
        bearGameObject.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(6.0f);
        LoadDialog6();
    }

    IEnumerator GameOverAnimation()
    {
        gameOverGameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenu");
    }

    public void endScene()
    {
        SceneManager.LoadScene("Scenes/Chapter2/HospitalScene");
    }
}

[Serializable]
public class ToyRoomProgress
{
    public bool beenThereBefore;

    public static ToyRoomProgress CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ToyRoomProgress>(jsonString);
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}