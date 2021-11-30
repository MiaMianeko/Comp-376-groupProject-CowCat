using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{

    [SerializeField] private Text save1Txt;
    [SerializeField] private Text save2Txt;
    [SerializeField] private Text save3Txt;
    [SerializeField] private SaveGameData save1; 
    [SerializeField] private SaveGameData save2;
    [SerializeField] private SaveGameData save3;
    private string save1String;
    private string save2String;
    private string save3String;
    private string save1SceneName;
    private string save2SceneName;
    private string save3SceneName;
    private int chop;
    private int chopEnd;
    void Start()
    {
        try
        {
            save1 = SaveGameData.CreateFromJSON(File.ReadAllText(Application.streamingAssetsPath + "/SaveData1.json"));
        }
        catch (Exception)
        {
            save1 = new SaveGameData();
            save1.scene = "";
        }
        try
        {
            save2 = SaveGameData.CreateFromJSON(File.ReadAllText(Application.streamingAssetsPath + "/SaveData2.json"));
        }
        catch (Exception)
        {
            save2 = new SaveGameData();
            save2.scene = "";
        }
        try
        {
            save3 = SaveGameData.CreateFromJSON(File.ReadAllText(Application.streamingAssetsPath + "/SaveData3.json"));

        }
        catch (Exception)
        {
            save3 = new SaveGameData();
            save3.scene = "";
        }
        if (save1.getScene() == "") save1String = "Save 1: No Data";
        else
        {

            save1SceneName = save1.getScene();
            chop = save1SceneName.LastIndexOf('/');
            if (chop> 0)
            chopEnd = save1SceneName.Length - chop - 6;
            if (chop > 0) save1SceneName = save1SceneName.Substring(chop+1, chopEnd);
       
            save1String = "Save 1: " + save1SceneName + "  " + save1.monthOfSave + "/" + save1.dayOfSave + " " + save1.hourOfSave + ":" + save1.minuteOfSave;
        }
        if (save2.getScene() == "") save2String = "Save 2: No Data";
        else
        {
            save2SceneName = save2.getScene();
            chop = save2SceneName.LastIndexOf('/');
            if (chop > 0)
                chopEnd = save2SceneName.Length - chop - 6;
            if (chop > 0) save2SceneName = save2SceneName.Substring(chop + 1, chopEnd);
            save2String = "Save 2: " + save2SceneName + "  " + save2.monthOfSave + "/" + save2.dayOfSave + " " + save2.hourOfSave + ":" + save2.minuteOfSave;
        }
        if (save3.getScene() == "") save3String = "Save 3: No Data";
        else
        {
            save3SceneName = save3.getScene();
           
            chop = save3SceneName.LastIndexOf('/');
            if (chop > 0)
                chopEnd = save3SceneName.Length - chop - 6;
            if (chop > 0) save3SceneName = save3SceneName.Substring(chop + 1, chopEnd);
            save3String = "Save 3: " + save3SceneName + "  " + save3.monthOfSave + "/" + save3.dayOfSave + " " + save3.hourOfSave + ":" + save3.minuteOfSave;
        }



        if (save1Txt != null)
        save1Txt.text = save1String;
        if (save2Txt != null)
            save2Txt.text = save2String;
        if (save3Txt != null)
            save3Txt.text = save3String;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void loadGame1Button()
    {
        if (save1.getScene() != "") SceneManager.LoadScene(save1.getScene());

    }
    public void loadGame2Button()
    {
        if (save2.getScene() != "") SceneManager.LoadScene(save2.getScene());

    }
    public void loadGame3Button()
    {
        if (save3.getScene() != "") SceneManager.LoadScene(save3.getScene());

    }

}



[Serializable]
public class SaveGameData
{
    public string scene;
    public int hourOfSave;
    public int minuteOfSave;
    public int dayOfSave;
    public int monthOfSave;

    public static SaveGameData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SaveGameData>(jsonString);
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
    public string getScene()
    {
        return (string)scene;
    }
}
