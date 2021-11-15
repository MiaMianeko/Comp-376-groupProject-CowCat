using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveGame : MonoBehaviour
{
    [SerializeField] string currentScene;

    [SerializeField] GameObject gameSavedMessage1;

    [SerializeField] GameObject gameSavedMessage2;

    [SerializeField] GameObject gameSavedMessage3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void saveGame1()
    {
        SaveGameData save = new SaveGameData();
        save.scene = currentScene;
        DateTime time = DateTime.Today;
        save.dayOfSave = time.Day;
        save.monthOfSave = time.Month;
        save.hourOfSave = time.Hour;
        save.minuteOfSave = time.Minute;

        string jsonSave = save.SaveToString();

        File.WriteAllText(Application.streamingAssetsPath + "/SaveData1.json", jsonSave);
        gameSavedMessage1.SetActive(true);

    }
    public void saveGame2()
    {
        SaveGameData save = new SaveGameData();
        save.scene = currentScene;
        DateTime time = DateTime.Today;
        save.dayOfSave = time.Day;
        save.monthOfSave = time.Month;
        save.hourOfSave = time.Hour;
        save.minuteOfSave = time.Minute;

        string jsonSave = save.SaveToString();

        File.WriteAllText(Application.streamingAssetsPath + "/SaveData2.json", jsonSave);
        gameSavedMessage2.SetActive(true);

    }
    public void saveGame3()
    {
        SaveGameData save = new SaveGameData();
        save.scene = currentScene;
        DateTime time = DateTime.Today;
        save.dayOfSave = time.Day;
        save.monthOfSave = time.Month;
        save.hourOfSave = time.Hour;
        save.minuteOfSave = time.Minute;

        string jsonSave = save.SaveToString();

        File.WriteAllText(Application.streamingAssetsPath + "/SaveData3.json", jsonSave);
        gameSavedMessage3.SetActive(true);

    }

}
