using UnityEngine;

public class ToStage1 : MonoBehaviour
{

    public void ToStageOne()
    {
        Debug.Log("I've been clicked");
        // if(GameManager.Instance.endLevel1 == false){
        Initiate.Fade("IntroMiniGame", Color.black, 1.0f);
        AudioInbetween.Instance.swapTrack(2);
        AudioInbetween.Instance.GetComponent<AudioSource>().mute = false;
        AudioInbetween.Instance.GetComponent<AudioSource>().Play();
        // }

    }

    public void ToStageTwo()
    {
        Initiate.Fade("Stage2", Color.black, 1.0f);
        AudioInbetween.Instance.swapTrack(2);
        AudioInbetween.Instance.GetComponent<AudioSource>().mute = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
