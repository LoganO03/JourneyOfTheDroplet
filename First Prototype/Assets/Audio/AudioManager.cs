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
    public void AdjustMasterVolume()
    {
        GameManager.Instance.AdjustVolume("Master", Mathf.Log10(MasterSlider.value) * 20);
    }

    public void AdjustMusicVolume()
    {
        GameManager.Instance.AdjustVolume("Music", Mathf.Log10(MusicSlider.value) * 20);
    }

    public void AdjustSFXVolume()
    {
        GameManager.Instance.AdjustVolume("SFX", Mathf.Log10(SFXSlider.value) * 20);
    }
}
