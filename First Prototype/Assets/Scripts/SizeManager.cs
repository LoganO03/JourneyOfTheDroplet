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


    void OnTriggerEnter2D(Collider2D other){
        currentScale += 0.2f;

        transform.localScale = new Vector3(currentScale, currentScale, 1);

        Destroy(other.gameObject);

        destroyedCount++;

        counterText.text = "Water Collected: " + destroyedCount + " / 20";

        if (destroyedCount >= 20)
        {
            Initiate.Fade("Beginning", Color.black, 1.0f);
        }
   }

    void Update(){
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(currentScale, currentScale, 1), Time.deltaTime * scaleSpeed);

   }
}
