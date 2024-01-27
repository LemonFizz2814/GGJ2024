using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Script References")]
    public UIManager uiManager;
    public PlayerScript playerScript;
    public FirstPersonController firstPersonController;
    [Space]
    [Header("Variables")]
    public float gameTime;

    // private variables
    private bool gameOver = false;
    private bool startTimer = false;
    private float timer;

    private void Start()
    {
        timer = gameTime;
    }

    public void GameStart()
    {
        SetStartTimer(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            float timeLeft = 1 - (timer / gameTime);
            uiManager.SetTimerSlider(timeLeft);

            if (timer <= 0)
            {
                // game ended
                GameOver("The wizard came home!");
            }
        }
    }

    public void GameOver(string _deathText)
    {
        if (!gameOver)
        {
            gameOver = true;
            uiManager.DisplayGameOver(true);
            uiManager.SetGameOverText(_deathText);
            playerScript.SetCanMove(false);
            firstPersonController.SetCanMove(false);
        }
    }

    public void GameWon()
    {
        gameOver = true;
        uiManager.DisplayGameWon(true);
    }

    public void SetStartTimer(bool _set)
    {
        startTimer = _set;
        playerScript.SetCanMove(true);
        firstPersonController.SetCanMove(true);
    }
}
