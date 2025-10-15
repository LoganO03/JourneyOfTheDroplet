using UnityEngine;

public class PlayGame : MonoBehaviour
{
    public bool inGame;
    public GameObject panel;
    public void turnOffPanel()
    {
        panel.SetActive(false);
    }

    public void ReactToClick()
    {
            Debug.Log("I've been clicked");
            Initiate.Fade("AtmHub", Color.black, 1.0f);
            AudioInbetween.Instance.GetComponent<AudioSource>().Stop();
       
            turnOffPanel();
        
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
