using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool menuIsActive;

    [SerializeField] private GameObject saveMenu;
    [SerializeField] private GameObject quitConfirm;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        menuIsActive = false;

        //pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    public void pauseGame()
    {
        Debug.Log("Inside Pause Game");

        menu.SetActive(true);
        Debug.Log("Inside Pause Game After Set Active Fuckery");

        menuIsActive = true;

        mainButtons.SetActive(true);
        Time.timeScale = 0.0f;

    }

    public void clickSaveButton()
    {
        saveMenu.SetActive(true);

        mainButtons.SetActive(false);

        quitConfirm.SetActive(false);

    }
    public void clickQuitButton()
    {
        saveMenu.SetActive(false);

        mainButtons.SetActive(false);

        quitConfirm.SetActive(true);

    }
    public void clickBackButton()
    {
        saveMenu.SetActive(false);

        mainButtons.SetActive(true);

        quitConfirm.SetActive(false);

    }
    public void clickQuitButtonConfirm()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/MainMenu");
    }
    public void clickResumeButton()
    {
        menuIsActive = false;
        saveMenu.SetActive(false);
        quitConfirm.SetActive(false);
        mainButtons.SetActive(false);
        Time.timeScale = 1;
        menu.gameObject.SetActive(false);

    }
}
