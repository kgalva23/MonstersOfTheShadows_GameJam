using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAudioController : MonoBehaviour
{
    [SerializeField] AudioSource freezeAudioSource;   // Audio source for the sound effect

    public void PlaySound()
    {
        if (freezeAudioSource != null && !freezeAudioSource.isPlaying) // Play if not already playing
        {
            freezeAudioSource.Play();
        }
    }
}
