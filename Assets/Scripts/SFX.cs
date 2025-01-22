using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    private static SFX sfx;
    public AudioClip click;
    public AudioClip Win;
    public AudioClip Lose;
    public AudioClip buy;
    public AudioClip bite;

    void Start()
    {
        SFXSource = GetComponent<AudioSource>();
    }

    

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
