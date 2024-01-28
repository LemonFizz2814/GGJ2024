using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static SoundManager;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        soundManager.PlaySound(Sounds.ButtonHover);
    }
}
