using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private Text TextComponent;

    void Awake()
    {
        TextComponent = GetComponent<Text>();
    }
    
    public void ShowDialog(string text)
    {
        StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        foreach (var item in text.ToCharArray())
        {
            TextComponent.text += item;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void ClearText()
    {
        TextComponent.text = "";
    }
}