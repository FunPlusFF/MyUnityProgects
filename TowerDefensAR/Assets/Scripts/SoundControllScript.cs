using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllScript : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}