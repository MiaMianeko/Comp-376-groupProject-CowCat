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
        switch (speaker)
        {
            case "Player":
                speakerImage.sprite = playerSprite;
                break;
            case "System":
                speakerImage.sprite = systemSprite;
                break;
            case "Sectary":
                speakerImage.sprite = sectarySprite;
                break;
            case "Professor":
                speakerImage.sprite = professorSprite;
                break;
        }
    }
}