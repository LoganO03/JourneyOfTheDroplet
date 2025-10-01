using System.Collections;
using UnityEngine;

public class TriggerSoundPlayer : MonoBehaviour
{
    public AudioSource Sound;
    private void start()
    {
        Sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Sound != null)
        {
            Sound.Play();
            Initiate.Fade("Stage1", Color.black, 1.0f);
            AudioInbetween.Instance.swapTrack(1);
            AudioInbetween.Instance.GetComponent<AudioSource>().mute = false;
        }

    }
   
        
}