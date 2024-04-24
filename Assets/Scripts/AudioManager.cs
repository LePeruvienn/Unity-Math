using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public GameObject BackgroundMusicObj;
    private AudioSource BackgroundMusicSource;


    [Header("Menu")]
    public List<AudioClip> buttonHoverList;
    public AudioClip buttonPressed;

    [Header("Game")]
    public AudioClip gameOver;
    public List<AudioClip> playerWalkingList;
    public AudioClip bonusTime;
    public AudioClip bonusSelect;
    public AudioClip zombieHit;
    public List<AudioClip> zombieDeathList;
    public AudioClip tireHead;
    public AudioClip aspireDebut;
    public AudioClip aspirePendant;
    public AudioClip aspireFin;

    void Start()
    {
        this.BackgroundMusicSource = BackgroundMusicObj.GetComponent<AudioSource>();
    }


    public void PlaySound(AudioClip otherClip)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = otherClip;
        audio.Play();
    }

    public void PlayBackGroundMusic()
    {
        this.BackgroundMusicSource.Play();
    }

    public void StopBackGroundMusic()
    {
        this.BackgroundMusicSource.Stop();
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
        PlaySound(bonusTime);
    }
    public void PlayBonusSelect()
    {
        PlaySound(bonusSelect);
    }

    public void PlayGameOver()
    {
        PlaySound(gameOver);
    }

    public void PlayTirehead()
    {
        PlaySound(tireHead);
    }

    public void PlayZombieDeath()
    {
        AudioClip audioClip = zombieDeathList[Random.Range(0, zombieDeathList.Count)];
        PlaySound(audioClip);
    }
}
