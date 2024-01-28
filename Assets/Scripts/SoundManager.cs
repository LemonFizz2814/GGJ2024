using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<SoundEffects> soundEffects = new List<SoundEffects>();

    private AudioSource audioSource;

    [Serializable]
    public struct SoundEffects
    {
        public Sounds sound;
        public AudioClip clip;
    }

    [Serializable]
    public enum Sounds
    {
        Grab, // done
        Throw, // done
        SolvedPuzzle, // done
        Incorrect, // done
        ItemInCauldron, // done
        WhoopeeCushion, // done
        Gameover, // done
        Gamewon, // done
        TimesUp, // done
        ButtonPressed, // done
        Drop, // done
        Dyanmite, // done
        Sandwich, // done
        ButtonHover, // 
        BananaMonsterWalk
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(Sounds _sound)
    {
        audioSource.PlayOneShot(GetClip(_sound));
    }

    AudioClip GetClip(Sounds _sound)
    {
        foreach(SoundEffects soundEffect in soundEffects)
        {
            if(soundEffect.sound == _sound)
            {
                return soundEffect.clip;
            }
        }

        Debug.LogError("MISSING SOUND BEING CALLED");
        return null;
    }
}
