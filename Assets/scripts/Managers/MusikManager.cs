using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusikManager : MonoBehaviour
{
    public AudioClip currentClip;
    public AudioClip[] musiks;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = musiks[Random.Range(0, 3)];
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            AudioClip clip = null;
            while (currentClip == clip)
            {
                clip = musiks[Random.Range(0, 3)];
            }
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
