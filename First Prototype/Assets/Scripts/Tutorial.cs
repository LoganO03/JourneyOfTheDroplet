using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    
    public int sec;

    public ToggleFade toggle;

    public VideoPlayer video;

    void Start()
    {
        Debug.Log("starting up");
        DoDelayAction();
        
    }

    void DoDelayAction()
    {
    StartCoroutine(DelayFadeIn());
    StartCoroutine(DelayFadeOut());
    
    }

    IEnumerator DelayFadeIn(){
        yield return new WaitForSeconds(3);
        toggle.ToggleActive();
        video.Play();
    }

    IEnumerator DelayFadeOut()
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(sec);
        toggle.ToggleActive();
        video.Pause();
    }


}
