using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource cue;
    public Slider forcePicker;

    public void PlayShotSound()
    {
        float shotForce = forcePicker.value;
        cue.volume = (shotForce / 100) * Settings.instance.GetVolume();
        cue.pitch = 1f + shotForce / 2000;
        cue.Play();
    }
}
