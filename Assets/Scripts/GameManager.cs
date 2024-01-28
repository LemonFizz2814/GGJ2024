using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SoundManager;

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
    public SoundManager soundManager;
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
        soundManager.PlaySound(Sounds.Intro);
    }

    public void GameStart()
    {
        SetStartTimer(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (startTimer && !gameOver)
        {
            timer -= Time.deltaTime;
            float timeLeft = 1 - (timer / gameTime);
            uiManager.SetTimerSlider(timeLeft);

            if (timer <= 0)
            {
                // spawn in wizard when time is up
                soundManager.PlaySound(Sounds.TimesUp);
                wizard.GetComponent<NavMeshAgent>().Warp(wizardSpawn.position);
                uiManager.DisplayWizardScreen(true);
                startTimer = false;
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

            soundManager.PlaySound(Sounds.Gameover);

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

        soundManager.PlaySound(Sounds.Gamewon);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        soundManager.PlaySound(Sounds.Ending);
    }

    public void SetStartTimer(bool _set)
    {
        startTimer = _set;
        playerScript.SetCanMove(true);
        firstPersonController.SetCanMove(true);
    }
}
