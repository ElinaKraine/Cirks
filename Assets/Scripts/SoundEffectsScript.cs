using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEfectScript : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public AudioSource audioSource;

    public void Hover()
    {
        audioSource.PlayOneShot(soundEffects[0]);
    }

    public void Click()
    {
        audioSource.PlayOneShot(soundEffects[1]);
    }

    public void OnDice()
    {
        audioSource.loop = true;
        audioSource.clip = soundEffects[2];
        audioSource.Play();
    }
}
