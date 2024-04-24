using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Menu")]
    public List<AudioClip> buttonHoverList;
    public AudioClip buttonPressed;

    [Header("Game")]
    public AudioClip gameOver;
    public List<AudioClip> playerWalkingList;
    public AudioClip bonusTime;
    public AudioClip bonusSelect;
    public AudioClip zombieHit;


    public void PlaySound(AudioClip otherClip)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = otherClip;
        audio.Play();
    }

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

    // GAME

    public void PlayPlayerWalking()
    {
        AudioClip audioClip = playerWalkingList[Random.Range(0, this.playerWalkingList.Count)];
        PlaySound(audioClip);
    }

    public void PlayDamage()
    {
        PlaySound(zombieHit);
    }

    public void PlayBonusTime()
    {
        PlaySound(zombieHit);
    }
    public void PlayBonusSelect()
    {
        PlaySound(bonusSelect);
    }

    public void PlayGameOver()
    {
        PlaySound(gameOver);
    }
}
