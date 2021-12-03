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
    [SerializeField] private UserController player;


    // Start is called before the first frame update
    void Start()
    {
        menuIsActive = false;

        //pauseMenu.SetActive(false);
    }

    // Update is called once per frame

    private void Update()
    {
        if (player.canMove && !menuIsActive && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
        else if (menuIsActive && Input.GetKeyDown(KeyCode.Escape)) clickResumeButton();
    }

    public void pauseGame()
    {
        player.canMove = false;
        menu.SetActive(true);
        menuIsActive = true;
        mainButtons.SetActive(true);
        quitConfirm.SetActive(false);
        saveMenu.SetActive(false);
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
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void clickResumeButton()
    {
        menuIsActive = false;
        saveMenu.SetActive(false);
        quitConfirm.SetActive(false);
        mainButtons.SetActive(false);
        player.canMove = true;
        menu.gameObject.SetActive(false);
    }
}