using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}