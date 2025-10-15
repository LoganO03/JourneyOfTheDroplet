using UnityEngine;

public class WaterPickup : MonoBehaviour
{
    AudioSource collectionSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        collectionSound = GameObject.FindGameObjectWithTag("WaterCollectionSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D (Collider2D other) {
        if(other.gameObject.layer == 6) { // player layer
           
                collectionSound.Play();
           
            float suckSpeed = 2 * Time.deltaTime; //other.transform.localScale.x / transform.localScale.x;
            float suckWater = (Mathf.Pow(transform.localScale.x, 3) * 4 * Mathf.PI) / 3 * suckSpeed;
            GameManager.Instance.playerWater += suckWater;
            transform.localScale = new Vector3(suckWater, suckWater, 1);
        }
        if(other.gameObject.layer == 4) { // water projectile layer
            float otherVolume = (other.transform.localScale.x * other.transform.localScale.y) * Mathf.PI * other.transform.localScale.z;
            float thisVolume = (Mathf.Pow(transform.localScale.x, 3) * 4  * Mathf.PI) / 3;
            float newRadius = (Mathf.Pow(thisVolume + otherVolume, 0.33333333f) * 3) / (Mathf.PI * 4);
            transform.localScale = new Vector3(newRadius, newRadius, 1);
            other.gameObject.SetActive(false);
        }
        /*if(other.gameObject.layer == 8) { // pickup layer
            if(transform.localScale.x > other.transform.localScale.x) {
                transform.localScale += other.transform.localScale;
                transform.position = (other.transform.position + transform.position) / 2;
                Destroy(other.gameObject);
            }
            
        }*/
    }

    void OnTriggerEnter2D (Collider2D other) {
    }
}
