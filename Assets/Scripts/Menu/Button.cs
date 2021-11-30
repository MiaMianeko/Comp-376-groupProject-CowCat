using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool selected;
    private Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = transform.GetComponentInChildren<Text>();
    }


    public void OnPointerEnter(PointerEventData data)
    {
        buttonText.color = Color.red;
    }

    public void OnPointerExit(PointerEventData data)
    {
        buttonText.color = Color.white;
    }

    public void OnPointerClick(PointerEventData data)
    {
        buttonText.color = Color.white;
    }


    public void clickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void clickSceneChange(string sceneName)
    {
        if (sceneName != "")
            SceneManager.LoadScene(sceneName);
    }
}