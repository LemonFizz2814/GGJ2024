using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gamerOverScreen;

    private void Start()
    {
        DisplayGameOver(false);
    }

    public void DisplayGameOver(bool _active)
    {
        gamerOverScreen.SetActive(_active);
    }
}
