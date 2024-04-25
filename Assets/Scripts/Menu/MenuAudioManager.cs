using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public GameObject BackgroundMusicObj;
    private AudioSource BackgroundMusicSource;


    [Header("Menu")]
    public List<AudioClip> buttonHoverList;
    public AudioClip buttonPressed;

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
}
