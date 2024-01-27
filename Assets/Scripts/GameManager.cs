using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Object References")]
    public GameObject exitDoor;
    public Transform wizardSpawn;
    public Transform wizard;
    [Space]
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
                // spawn in wizard when time is up
                wizard.position = wizardSpawn.position;
                uiManager.DisplayWizardScreen(true);
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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CauldronSolved()
    {
        exitDoor.SetActive(false);
    }

    public void GameWon()
    {
        gameOver = true;
        uiManager.DisplayGameWon(true);
        playerScript.SetCanMove(false);
        firstPersonController.SetCanMove(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetStartTimer(bool _set)
    {
        startTimer = _set;
        playerScript.SetCanMove(true);
        firstPersonController.SetCanMove(true);
    }
}
