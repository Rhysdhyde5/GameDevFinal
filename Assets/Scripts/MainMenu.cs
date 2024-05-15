using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void optionsSettings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void backToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void toTutorial()
    {
        SceneManager.LoadScene("Tutorial1");
    }

    public void resetGameMenu()
    {
        SaveSystem.SavePlayer(0, 3, 0, 0, 0, 0, 0);
        SceneManager.LoadScene("MainGame");
    }

}
