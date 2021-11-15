using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FileReader
{
    public static IEnumerator GetText(string url, Action<string> taskCompletedCallBack)
    {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX || UNITY_STANDALONE_LINUX || UNITY_EDITOR_LINUX || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        url = "file://" + url;
#endif
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