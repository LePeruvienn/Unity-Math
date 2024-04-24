using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public GameObject BackgroundMusicObj;
    private AudioSource BackgroundMusicSource;

    public GameObject PlayerAudioObj;
    private AudioSource PlayerAudioSource;

    public GameObject WeaponAudioObj;
    private AudioSource WeaponAudioSource;

    public GameObject ZombieheadAudioObj;
    private AudioSource ZombieheadAudioSource;


    [Header("Menu")]
    public List<AudioClip> buttonHoverList;
    public AudioClip buttonPressed;

    [Header("Game")]
    public AudioClip gameOver;
    public List<AudioClip> playerWalkingList;
    public AudioClip bonusTime;
    public AudioClip bonusSelect;
    public AudioClip zombieSpawn;

    void Start()
    {
        this.BackgroundMusicSource = BackgroundMusicObj.GetComponent<AudioSource>();

        this.PlayerAudioSource = PlayerAudioObj.GetComponent<AudioSource>();

        this.WeaponAudioSource = WeaponAudioObj.GetComponent<AudioSource>();

        this.ZombieheadAudioSource = ZombieheadAudioObj.GetComponent<AudioSource>();
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

    public void PlayZombieSpawn()
    {
        this.ZombieheadAudioSource.clip = zombieSpawn;
        this.ZombieheadAudioSource.Play();
    }
}
