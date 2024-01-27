using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    public GameObject startScreen;

    public RectTransform crosshair;
    public RadialSlider timerSlider;

    public TextMeshProUGUI gameOverText;

    private void Start()
    {
        DisplayGameOver(false);
        DisplayGameWon(false);
    }

    public void DisplayGameOver(bool _active)
    {
        gameOverScreen.SetActive(_active);
    }
    public void DisplayGameWon(bool _active)
    {
        gameWonScreen.SetActive(_active);
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
        startScreen.SetActive(_active);
        gameManager.GameStart();
    }
}
