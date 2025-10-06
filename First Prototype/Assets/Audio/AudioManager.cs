using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

//Thanks to Kap Koder's "AUDIO MIXERS In Unity" for the info on how to make this work

public class AudioManager : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
    }
    public void AdjustMasterVolume()
    {
        Debug.Log("Adjusting master volume.");
        AudioInbetween.Instance.AdjustVolume("Master", Mathf.Log10(MasterSlider.value) * 20);
    }

    public void AdjustMusicVolume()
    {
        Debug.Log("Adjusting music volume.");
        AudioInbetween.Instance.AdjustVolume("Music", Mathf.Log10(MusicSlider.value) * 20);
    }

    public void AdjustSFXVolume()
    {
        Debug.Log("Adjusting SFX volume.");
        AudioInbetween.Instance.AdjustVolume("SFX", Mathf.Log10(SFXSlider.value) * 20);
    }
}
