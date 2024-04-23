using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Menu")]
    public List<AudioClip> buttonHoverList;
    public AudioClip buttonPressed;

    [Header("Game")]
    public List<AudioClip> playerWalkingList;
    public AudioClip BonusTime;
    public AudioClip BonusSelect;
    public AudioClip BonusHover;

    // MENU
    public void PlayButtonHover()
    {
        AudioClip audioClip = buttonHoverList[Random.Range(0, this.buttonHoverList.Count)];
        PlaySound(audioClip);
    }

    public void PlayButtonPressed()
    {
        PlaySound(buttonPressed);
    }

    public void PlaySound(AudioClip otherClip)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = otherClip;
        audio.Play();
    }

    // GAME

    public void PlayPlayerWalking()
    {
        AudioClip audioClip = playerWalkingList[Random.Range(0, this.playerWalkingList.Count)];
        PlaySound(audioClip);
    }
}
