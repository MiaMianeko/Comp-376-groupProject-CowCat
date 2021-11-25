using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachinePuzzle : MonoBehaviour
{

    [SerializeField] Text firstDigitText;
    [SerializeField] Text secondDigitText;
    [SerializeField] Text thirdDigitText;
    [SerializeField] Text fourthDigitText;

    [SerializeField] GameObject dialogGameObject;

    [SerializeField] GameObject buttons;

    UserInput player;
    HospitalManager manager;
    Dialog _dialog;


    int firstDigit = 0;
    int secondDigit = 0;
    int thirdDigit = 0;
    int fourthDigit = 0;

    bool isSolved;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<HospitalManager>();
        player = FindObjectOfType<UserInput>();
    }

    // Update is called once per frame
    void Update()
    {
        firstDigitText.text = firstDigit.ToString();
        secondDigitText.text = secondDigit.ToString();
        thirdDigitText.text = thirdDigit.ToString();
        fourthDigitText.text = fourthDigit.ToString();

        if (!isSolved && firstDigit == 1 && secondDigit == 9 && thirdDigit == 2 && fourthDigit == 8) solvePuzzle();

    }

    public void incrementDigit(int digit)
    {
        bool increment = true;
        switch (digit){
            case 1:
                if (increment)
                {
                    firstDigit++;
                    if (firstDigit > 9) firstDigit = 0;
                }
                else
                {
                    firstDigit--;
                    if (firstDigit < 0) firstDigit = 9;
                }
                break;
            case 2:
                if (increment)
                {
                    secondDigit++;
                    if (secondDigit > 9) secondDigit = 0;
                }
                else
                {
                    secondDigit--;
                    if (secondDigit < 0) secondDigit = 9;
                }
                break;
            case 3:
                if (increment)
                {
                    thirdDigit++;
                    if (thirdDigit > 9) thirdDigit = 0;
                }
                else
                {
                    thirdDigit--;
                    if (thirdDigit < 0) thirdDigit = 9;
                }
                break;
            case 4:
                if (increment)
                {
                    fourthDigit++;
                    if (fourthDigit > 9) fourthDigit = 0;
                }
                else
                {
                    fourthDigit--;
                    if (fourthDigit < 0) fourthDigit = 9;
                }
                break;
        }
    }
    public void decrementDigit(int digit)
    {
        bool increment = false;
        switch (digit)
        {
            case 1:
                if (increment)
                {
                    firstDigit++;
                    if (firstDigit > 9) firstDigit = 0;
                }
                else
                {
                    firstDigit--;
                    if (firstDigit < 0) firstDigit = 9;
                }
                break;
            case 2:
                if (increment)
                {
                    secondDigit++;
                    if (secondDigit > 9) secondDigit = 0;
                }
                else
                {
                    secondDigit--;
                    if (secondDigit < 0) secondDigit = 9;
                }
                break;
            case 3:
                if (increment)
                {
                    thirdDigit++;
                    if (thirdDigit > 9) thirdDigit = 0;
                }
                else
                {
                    thirdDigit--;
                    if (thirdDigit < 0) thirdDigit = 9;
                }
                break;
            case 4:
                if (increment)
                {
                    fourthDigit++;
                    if (fourthDigit > 9) fourthDigit = 0;
                }
                else
                {
                    fourthDigit--;
                    if (fourthDigit < 0) fourthDigit = 9;
                }
                break;
        }
    }

    public void solvePuzzle()
    {
        isSolved = true;
        buttons.SetActive(false);

        dialogGameObject.SetActive(true);
        _dialog = FindObjectOfType<Dialog>();
        manager.machinePuzzleSolved = true;


        StartCoroutine(FileReader.GetText(
        Application.streamingAssetsPath + "/Dialogs/MachinePuzzleSolved.json",
        jsonData =>
        {
            DialogData dialogData = JsonUtility.FromJson<DialogData>(jsonData);
            StartCoroutine(_dialog.OutputDialog(dialogData, () =>
            {
                dialogGameObject.SetActive(false);
                this.gameObject.SetActive(false);
                player.canMove = true;
            }));
        }));

    }

}
