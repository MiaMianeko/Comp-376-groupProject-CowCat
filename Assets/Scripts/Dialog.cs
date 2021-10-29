using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private Text TextComponent;
    [SerializeField] private GameObject speakerGameObject;

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
            yield return new WaitForSeconds(0.04f);
        }
    }

    public void ClearText()
    {
        TextComponent.text = "";
    }

    public void SetSpeaker(string speaker)
    {
        speakerGameObject.GetComponent<Text>().text = speaker;
    }
}