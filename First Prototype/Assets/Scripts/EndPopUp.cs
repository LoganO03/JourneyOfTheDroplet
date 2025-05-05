using System.Collections;
using UnityEngine;

public class EndPopUp : MonoBehaviour
{
    public int sec;

    public ToggleFade toggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("starting fade");
        DoDelayAction();
        
    }

    void DoDelayAction()
    {
        StartCoroutine(DelayFadeIn());
        StartCoroutine(DelayFadeOut());
        
    
    }

    IEnumerator DelayFadeIn(){
        yield return new WaitForSeconds(sec);
        toggle.ToggleActive();
        
    }

    IEnumerator DelayFadeOut()
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(sec + 5);
        Initiate.Fade("AtmHub", Color.black, 1.0f);
        
    }
}
