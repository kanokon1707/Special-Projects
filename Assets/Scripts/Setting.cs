using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    [SerializeField] private Image soundOnIcon, soundOffIcon, soundOnBttn, soundOffBttn;
    [SerializeField] private Image sfxOnIcon, sfxOffIcon, sfxOnBttn, sfxOffBttn;
    [SerializeField] private Image saveButtonImage;  // Image of the Save button
    [SerializeField] private Sprite savedSprite;     // Sprite to show after saving
    [SerializeField] private Sprite defaultSprite;   // Default sprite for the Save button
    [SerializeField] private Image muteMusicSliderImage;
    [SerializeField] private Image muteSFXSliderImage;

    private bool musicMuted = false;
    private bool sfxMuted = false;
    private bool isSaved = false;
    private float currentMusicVolume = 1f;
    private float currentSFXVolume = 1f;

    SFX sFX;


    void Start()
    {
        Load();
        UpdateUIMusic();
        UpdateUISFX();
        UpdateSaveButtonState();
    }

    private void Awake()
    {
        sFX = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFX>();
    }

    public void ChangeMusicVolume()
    {
     // Update the currentMusicVolume directly from the slider value
    currentMusicVolume = MusicSlider.value;

    // Set the volume on the AudioMixer
    myMixer.SetFloat("Music", Mathf.Log10(currentMusicVolume) * 20 );

    // Update mute state
    musicMuted = currentMusicVolume <= 0;

    UpdateUIMusic(); // Update the UI
    isSaved = false; // Mark as unsaved
    UpdateSaveButtonState();
    }

    public void ChangeSFXVolume()
    {
    // Update the currentSFXVolume directly from the slider value
    currentSFXVolume = SFXSlider.value;

    // Set the volume on the AudioMixer
    myMixer.SetFloat("SFX", Mathf.Log10(currentSFXVolume) * 20 );

    // Update mute state
    sfxMuted = currentSFXVolume <= 0;

    UpdateUISFX(); // Update the UI
    isSaved = false; // Mark as unsaved
    UpdateSaveButtonState();
    }

     public void ToggleMusic()
    {
        // Toggle music mute
        musicMuted = !musicMuted;
    
        if (musicMuted)
        {
            myMixer.SetFloat("Music", -80f);
            MusicSlider.value = 0;
        }
        else
        {
            myMixer.SetFloat("Music", Mathf.Log10(currentMusicVolume) * 20);
            MusicSlider.value = currentMusicVolume;
        }

        UpdateUIMusic();
        isSaved = false;
        UpdateSaveButtonState();
       
    }

     public void ToggleSFX()
    {
        // Toggle SFX mute
        sfxMuted = !sfxMuted;

        if (sfxMuted)
        {
            myMixer.SetFloat("SFX", -80f);
            SFXSlider.value = 0;
        }
        else
        {
            myMixer.SetFloat("SFX", Mathf.Log10(currentSFXVolume) * 20);
            SFXSlider.value = currentSFXVolume;
        }

        UpdateUISFX();
        isSaved = false;
        UpdateSaveButtonState();
    }


    public void OnSaveButtonPress()
    {
        sFX.PlaySFX(sFX.click);
        Save();
        isSaved = true;
        UpdateSaveButtonState();
    }

    public void OnCloseButtonPress()
    {
        sFX.PlaySFX(sFX.click);
        gameObject.SetActive(false);
    }

    private void UpdateUIMusic()
    {
        soundOnIcon.enabled = !musicMuted;
        soundOnBttn.enabled = !musicMuted;
        soundOffIcon.enabled = musicMuted;
        soundOffBttn.enabled = musicMuted;

        muteMusicSliderImage.enabled = musicMuted;
        MusicSlider.gameObject.SetActive(!musicMuted);
    }

    private void UpdateUISFX()
    {
        sfxOnIcon.enabled = !sfxMuted;
        sfxOnBttn.enabled = !sfxMuted;
        sfxOffIcon.enabled = sfxMuted;
        sfxOffBttn.enabled = sfxMuted;

        muteSFXSliderImage.enabled = sfxMuted;
        SFXSlider.gameObject.SetActive(!sfxMuted);
    }

    private void UpdateSaveButtonState()
    {
        saveButtonImage.sprite = isSaved ? savedSprite : defaultSprite;
    }

    private void Load()
    {
        // Load volumes from PlayerPrefs
        currentMusicVolume = PlayerPrefs.GetFloat("Music", 1f);
        currentSFXVolume = PlayerPrefs.GetFloat("SFX", 1f);
        musicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        sfxMuted = PlayerPrefs.GetInt("SFXMuted", 0) == 1;

        // Apply volumes to AudioMixer
        // myMixer.SetFloat("Music", Mathf.Log10(currentMusicVolume) * 20);
        // myMixer.SetFloat("SFX", Mathf.Log10(currentSFXVolume) * 20);

        myMixer.SetFloat("Music", musicMuted ? -80f : Mathf.Log10(currentMusicVolume)*20);
        myMixer.SetFloat("SFX", musicMuted ? -80f : Mathf.Log10(currentSFXVolume)*20);

        MusicSlider.value = musicMuted ? 0 : currentMusicVolume;
        SFXSlider.value = sfxMuted ? 0 : currentSFXVolume;

        // musicMuted = currentMusicVolume <= 0;
        // sfxMuted = currentSFXVolume <= 0;

        UpdateUIMusic();
        UpdateUISFX();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void OnDisable()
    {
        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("Music", currentMusicVolume);
        PlayerPrefs.SetFloat("SFX", currentSFXVolume);
        PlayerPrefs.SetInt("MusicMuted", musicMuted ? 1 : 0);
        PlayerPrefs.SetInt("SFXMuted", sfxMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

}