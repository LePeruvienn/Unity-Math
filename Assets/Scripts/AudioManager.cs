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

    public GameObject AspiAudioObj;
    private AudioSource AspiAudioSource;

    public GameObject ZombieheadAudioObj;
    private AudioSource ZombieheadAudioSource;

    public GameObject ModeAudioObj;
    private AudioSource ModeAudioSource;


    [Header("Menu")]
    public List<AudioClip> buttonHoverList;
    public AudioClip buttonPressed;

    [Header("Game")]
    public AudioClip gameOver;
    public List<AudioClip> playerWalkingList;
    public AudioClip bonusTime;
    public AudioClip bonusSelect;
    public AudioClip zombieSpawn;
    public AudioClip modeSwitch;
    public AudioClip aspiShoot;

    [Header("Aspiration")]
    public AudioClip aspiDebut;
    public AudioClip aspiPedant;
    public AudioClip aspiFin;

    void Start()
    {

        this.AspiAudioSource = AspiAudioObj.GetComponent<AudioSource>();

        this.PlayerAudioSource = PlayerAudioObj.GetComponent<AudioSource>();

        this.WeaponAudioSource = WeaponAudioObj.GetComponent<AudioSource>();

        this.WeaponAudioSource = WeaponAudioObj.GetComponent<AudioSource>();

        this.ZombieheadAudioSource = ZombieheadAudioObj.GetComponent<AudioSource>();

        this.ModeAudioSource = ModeAudioObj.GetComponent<AudioSource>();
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
    public void PlayModeSwitch()
    {
        this.ModeAudioSource.clip = modeSwitch;
        this.ModeAudioSource.Play();
    }

    public void PlayAspiShoot()
    {
        this.WeaponAudioSource.clip = aspiShoot;
        this.WeaponAudioSource.Play();
    }
    
    public void PlayAspiDebut()
    {
        this.AspiAudioSource.loop = false;
        this.AspiAudioSource.clip = aspiDebut;
        this.AspiAudioSource.Play();
    }

    public void PlayAspiPendant()
    {
        this.AspiAudioSource.loop = true;
        this.AspiAudioSource.clip = aspiPedant;
        this.AspiAudioSource.Play();
    }

    public void PlayAspiFin()
    {
        this.AspiAudioSource.loop = false;
        this.AspiAudioSource.clip = aspiFin;
        this.AspiAudioSource.Play();
    }

    public bool CanPlayAspiPendant()
    {
        return !this.AspiAudioSource.isPlaying && this.AspiAudioSource.clip == this.aspiDebut;
    }
}
