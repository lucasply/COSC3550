using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMaster : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject LevelSelector;
    public GameObject ExitMenu;
    
    /* =========================
    //  Starting Screen Buttons
    // ========================= */

    // Starting stuff
    public void PlayGame()
    {
        LevelSelector.SetActive(true);
    }
    public void back()
    {
        LevelSelector.SetActive(false);
    }
    
    public void Map1()
    {
        SceneManager.LoadScene("Map1");
    }

    // Settings stuff
    public void Settings()
    {
        SettingsMenu.SetActive(true);
    }
    public void ExitSettings()
    {
        SettingsMenu.SetActive(false);
    }

    // Exit stuff
    public void ExitGame()
    {
        ExitMenu.SetActive(true);
    }
    public void ExitYes()
    {
        Application.Quit();
        Debug.Log("Quiting Game...");
    }
    public void ExitNo()
    {
        ExitMenu.SetActive(false);
    }

    /* =========================
    //  Game Over Screen Buttons
    // ========================= */

    public void PlayAgain()
    {
        // Restart the game
        SceneManager.LoadScene("Map1");
    }

    /* =========================
    //  Victory Screen Buttons
    // ========================= */

    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("_TitleScreen");
    }
}
