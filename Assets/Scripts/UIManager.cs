using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using static SoundManager;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject gameScreen;
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    public GameObject startScreen;
    public GameObject wizardScreen;

    public Animator stopWatchAnimator;

    public RectTransform crosshair;
    public RadialSlider timerSlider;

    public TextMeshProUGUI gameOverText;

    public SoundManager soundManager;

    private void Start()
    {
        startScreen.SetActive(true);
        gameScreen.SetActive(true);
        DisplayGameOver(false);
        DisplayGameWon(false);
        DisplayWizardScreen(false);
    }

    public void DisplayGameOver(bool _active)
    {
        gameOverScreen.SetActive(_active);
    }
    public void DisplayGameWon(bool _active)
    {
        gameWonScreen.SetActive(_active);
    }
    public void DisplayWizardScreen(bool _active)
    {
        wizardScreen.SetActive(_active);
    }

    public void ScaleCrosshair(float _scale)
    {
        crosshair.localScale = new Vector3(_scale, _scale, _scale);
    }
    public void SetTimerSlider(float _fill)
    {
        timerSlider.SetFillAmount(_fill);
    }
    public void SetGameOverText(string _text)
    {
        // only change text if told to change it
        if(_text != "")
        {
            gameOverText.text = _text;
        }
    }

    public void StartPressed(bool _active)
    {
        soundManager.PlaySound(Sounds.ButtonPressed);
        startScreen.SetActive(_active);
        gameManager.GameStart();
        stopWatchAnimator.SetTrigger("Start");
    }

    public void RestartPressed()
    {
        soundManager.PlaySound(Sounds.ButtonPressed);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitPressed()
    {
        soundManager.PlaySound(Sounds.ButtonPressed);
        SceneManager.LoadScene("MainMenu");
    }
}