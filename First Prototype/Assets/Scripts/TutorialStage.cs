using System.Collections;
using UnityEngine;

public class TutorialStage : MonoBehaviour
{
    
    public int sec;

    public ToggleFade toggle;

    public GameObject character;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == character.name){
            if (!triggered){
            DoDelayAction();
            triggered = true;
        }
        }
        
    
    }

    void DoDelayAction()
    {
    StartCoroutine(DelayFadeIn());
    StartCoroutine(DelayFadeOut());
    
    }

    IEnumerator DelayFadeIn(){
        yield return new WaitForSeconds(1);
        toggle.ToggleActive();
    }

    IEnumerator DelayFadeOut()
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(sec);
        toggle.ToggleActive();
    }


}
