using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAudioController : MonoBehaviour
{
    [SerializeField] AudioSource lightningAudioSource;   // Audio source for the sound effect

    public void PlaySound()
    {
        if (lightningAudioSource != null && !lightningAudioSource.isPlaying) // Play if not already playing
        {
            lightningAudioSource.Play();
        }
    }
}
