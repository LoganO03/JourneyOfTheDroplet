using UnityEngine;

public class SizeManager : MonoBehaviour
{
    private float currentScale = 1f;
    public float scaleSpeed = 5f;

    private int destroyedCount = 0;

    void OnTriggerEnter2D(Collider2D other){
        currentScale += 0.2f;

        transform.localScale = new Vector3(currentScale, currentScale, 1);

        Destroy(other.gameObject);

        destroyedCount++;

        if (destroyedCount >= 20)
        {
            Initiate.Fade("Stage1", Color.black, 1.0f);
        }
   }

    void Update(){
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(currentScale, currentScale, 1), Time.deltaTime * scaleSpeed);

   }
}
