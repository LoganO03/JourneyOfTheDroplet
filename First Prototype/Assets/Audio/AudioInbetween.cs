using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioInbetween : MonoBehaviour
{
    public AudioMixer audioMixer;
    public List<AudioClip> musicTracks;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static AudioInbetween Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AdjustVolume(string mixerName, float level)
    {
        Debug.Log("AudioInbetween instance found!");
        audioMixer.SetFloat(mixerName, level);
    }

    public void swapTrack(int index)
    {
        GetComponent<AudioSource>().clip = musicTracks[index];
     
    }
}
