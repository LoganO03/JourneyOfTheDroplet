using UnityEngine;

public class Puddle : MonoBehaviour
{
    private Vector3 fullHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        fullHeight = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y > fullHeight.y) {
            transform.localPosition -= Vector3.up * (transform.localPosition.y - fullHeight.y);
        }
    }

    void OnTriggerStay2D (Collider2D other) {
        if(other.gameObject.layer == 6) { // player layer
            float suckSpeed = 2 * Time.deltaTime * other.transform.localScale.x; //other.transform.localScale.x / transform.localScale.x;
            float suckWater = (transform.localScale.x * transform.localScale.y) * suckSpeed;
            GameManager.Instance.playerWater += suckWater;
            transform.localPosition -= Vector3.up * suckWater / transform.localScale.z;
        }
        if(other.gameObject.layer == 8 && other.transform.localScale.x > 0.01f) { // pickup layer
            float suckSpeed = 2 * Time.deltaTime / other.transform.localScale.z; //other.transform.localScale.x / transform.localScale.x;
            float suckWater = suckSpeed / transform.localScale.z;
            transform.localPosition += Vector3.up * (suckWater);
            other.transform.localScale -= new Vector3(other.transform.localScale.x, other.transform.localScale.y, 0) * (suckWater / (other.transform.localScale.x * other.transform.localScale.y * other.transform.localScale.z));
        }
        if(other.gameObject.layer == 4) { // water projectile layer
            float otherVolume = (other.transform.localScale.x * other.transform.localScale.y) * Mathf.PI * other.transform.localScale.z;
            transform.localPosition += Vector3.up * otherVolume / (transform.localScale.x * transform.localScale.y * transform.localScale.z);
        }
    }

    public void getFilld (Collider2D other) {
        if(other.gameObject.layer == 8) { // pickup layer
            float suckSpeed = 2 * Time.deltaTime / other.transform.localScale.z; //other.transform.localScale.x / transform.localScale.x;
            float suckWater = suckSpeed / transform.localScale.z;
            transform.localPosition += Vector3.up * (suckWater);
            other.transform.localScale -= new Vector3(other.transform.localScale.x, other.transform.localScale.y, 0) * (suckWater / (other.transform.localScale.x * other.transform.localScale.y * other.transform.localScale.z));
        }
        if(other.gameObject.layer == 4) { // water projectile layer
            float otherVolume = (other.transform.localScale.x * other.transform.localScale.y) * Mathf.PI * other.transform.localScale.z;
            transform.localPosition += Vector3.up * otherVolume / (transform.localScale.x * transform.localScale.y * transform.localScale.z);
        }
    
    }
    public float waterFillRate = 0.1f; // adjustable rate of water added per second

    public void AddWaterOverTime(){
        // Increase the scale of the puddle gradually over time
        float amountToAdd = waterFillRate * Time.deltaTime;

        // Increase the puddle's scale on x and y (width and height)
        transform.localScale += new Vector3(amountToAdd, amountToAdd, 0);

        // Move the puddle upwards visually to simulate filling
        transform.localPosition += Vector3.up * (amountToAdd / transform.localScale.z);
}


}
