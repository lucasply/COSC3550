using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMaster : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject LevelSelector;
    public GameObject ExitMenu;
    
    // Starting stuff
    public void PlayGame()
    {
        LevelSelector.SetActive(true);
    }
    public void back()
    {
        LevelSelector.SetActive(false);
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


}
