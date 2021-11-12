using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private Text TextComponent;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite systemSprite;
    [SerializeField] private Sprite sectarySprite;
    [SerializeField] private Sprite professorSprite;
    [SerializeField] private Image speakerImage;

    void Awake()
    {
        TextComponent = GetComponent<Text>();
    }

    public IEnumerator TypeText(string text)
    {
        float t = 0;
        int charIndex = 0;
        while (charIndex < text.Length)
        {
            t += Time.deltaTime * 50;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);
            TextComponent.text = text.Substring(0, charIndex);
            yield return null;
        }

        TextComponent.text = text;
    }

    public void ClearText()
    {
        TextComponent.text = "";
    }

    public void SetSpeaker(string speaker)
    {
        switch (speaker)
        {
            case "Player":
                speakerImage.sprite = playerSprite;
                break;
            case "System":
                speakerImage.sprite = systemSprite;
                break;
            case "Assistant":
                speakerImage.sprite = sectarySprite;
                break;
            case "Professor":
                speakerImage.sprite = professorSprite;
                break;
        }
    }

    public IEnumerator OutputDialog(DialogData dialogData, Action callback)
    {
        foreach (var jsonDialogData in dialogData.data)
        {
            SetSpeaker(jsonDialogData.speaker);
            ClearText();
            yield return TypeText(jsonDialogData.content);
            yield return new WaitUntil(() => Input.GetButtonDown("Skip"));
        }

        callback();
    }
}