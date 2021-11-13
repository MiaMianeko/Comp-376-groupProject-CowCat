using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinPad : MonoBehaviour
{
    [SerializeField] private Text codeText;

    private string codeTextValue;
    private int tryCount;

    public PinPad()
    {
        codeTextValue = "";
        tryCount = 0;
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
            gameObject.SetActive(false);
            FindObjectOfType<ChapterOneClassRoomGameManager>().LoadDialog7();
        }
        else if (codeTextValue.Length >= 4)
        {
            tryCount++;
            codeTextValue = "";
            gameObject.SetActive(false);
            FindObjectOfType<ChapterOneClassRoomGameManager>().LoadDialog3(tryCount >= 3);
        }
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }
}