using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip buttonHover;
    public AudioClip buttonPressed;
    public AudioClip menuMusic;

    public void PlayButtonHover()
    {
        PlaySound(buttonHover);

    }

    public void PlayButtonPressed()
    {
        PlaySound(buttonPressed);
    }

    public IEnumerator PlaySound(AudioClip otherClip)
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = otherClip;
        audio.Play();
    }
}
