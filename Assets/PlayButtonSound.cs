using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip clickExitSound;

    public void playHoverSound()
    {
        PlaySound(hoverSound);
    }

    public void playClickSound()
    {
        PlaySound(clickSound);
    }

    public void playClickExitSound()
    {
        PlaySound(clickExitSound);
    }

    void PlaySound(AudioClip sound)
    {
        audioSource.volume = Settings.instance.GetVolume();
        audioSource.PlayOneShot(sound);
    }
}
