using System.Collections;
using UnityEngine;
using TMPro;

public class SizeManager : MonoBehaviour
{

    //Controls the Size for the Intro Condensation Minigame
    private float currentScale = 1f;
    public float scaleSpeed = 5f;

    private int destroyedCount = 0;
    public TextMeshProUGUI counterText;
    AudioSource collectionSound;

    public TopDownMovement movementManager;

    void Start()
    {
        collectionSound = GameObject.FindGameObjectWithTag("WaterCollectionSound").GetComponent<AudioSource>();
}

    void OnTriggerEnter2D(Collider2D other){
        currentScale += 0.10f;
        movementManager.increaseSpeed();

        transform.localScale = new Vector3(currentScale, currentScale, 1);

        Destroy(other.gameObject);

        destroyedCount++;

        counterText.text = "Water Collected: " + destroyedCount + " / 30";

        float variance = Random.Range(-.55f, .5f);
        collectionSound.pitch = (float)(2.5 + variance);
        collectionSound.Play();

        if (destroyedCount >= 30)
        {
            Initiate.Fade("IntroLeadIn", Color.black, 1.0f);
            AudioInbetween.Instance.GetComponent<AudioSource>().Stop();
        }
   }

    void Update(){
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(currentScale, currentScale, 1), Time.deltaTime * scaleSpeed);

   }
}
