using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinPad : MonoBehaviour
{
    [SerializeField] private Text codeText;

    private string codeTextValue;

    public PinPad()
    {
        codeTextValue = "";
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        codeText.text = codeTextValue;
        if (codeTextValue == "3850")
        {
            FindObjectOfType<ChapterOneClassRoomGameManager>().isChangeScene = true;
        }
        else if (codeTextValue.Length >= 4)
        {
            codeTextValue = "";
            gameObject.SetActive(false);
            FindObjectOfType<ChapterOneClassRoomGameManager>().LoadDialog3();
        }
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }
}