using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveGame : MonoBehaviour
{
    [SerializeField] string currentScene;

    [SerializeField] GameObject gameSavedMessage1;
    private float save1time = 0f;
    [SerializeField] GameObject gameSavedMessage2;
    private float save2time = 0f;
    [SerializeField] GameObject gameSavedMessage3;
    private float save3time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (gameSavedMessage1.activeSelf && Time.time > save1time + 2f) gameSavedMessage1.SetActive(false);
        if (gameSavedMessage2.activeSelf && Time.time > save2time + 2f) gameSavedMessage2.SetActive(false);
        if (gameSavedMessage3.activeSelf && Time.time > save3time + 2f) gameSavedMessage3.SetActive(false);
    }


    public void saveGame1()
    {
        SaveGameData save = new SaveGameData();
        save.scene = currentScene;
        DateTime time = DateTime.Now;
        save.dayOfSave = time.Day;
        save.monthOfSave = time.Month;
        save.hourOfSave = time.Hour;
        save.minuteOfSave = time.Minute;

        string jsonSave = save.SaveToString();

        File.WriteAllText(Application.streamingAssetsPath + "/SaveData1.json", jsonSave);
        gameSavedMessage1.SetActive(true);
        save1time = Time.time;
    }
    public void saveGame2()
    {
        SaveGameData save = new SaveGameData();
        save.scene = currentScene;
        DateTime time = DateTime.Now;
        save.dayOfSave = time.Day;
        save.monthOfSave = time.Month;
        save.hourOfSave = time.Hour;
        save.minuteOfSave = time.Minute;

        string jsonSave = save.SaveToString();

        File.WriteAllText(Application.streamingAssetsPath + "/SaveData2.json", jsonSave);
        gameSavedMessage2.SetActive(true);
        save2time = Time.time;
    }
    public void saveGame3()
    {
        SaveGameData save = new SaveGameData();
        save.scene = currentScene;
        DateTime time = DateTime.Now;
        save.dayOfSave = time.Day;
        save.monthOfSave = time.Month;
        save.hourOfSave = time.Hour;
        save.minuteOfSave = time.Minute;

        string jsonSave = save.SaveToString();

        File.WriteAllText(Application.streamingAssetsPath + "/SaveData3.json", jsonSave);
        gameSavedMessage3.SetActive(true);
        save3time = Time.time;
    }

}
