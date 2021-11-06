using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FileReader
{
    public static IEnumerator GetText(string url, Action<string> taskCompletedCallBack)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            taskCompletedCallBack(www.downloadHandler.text);
        }
    }
}