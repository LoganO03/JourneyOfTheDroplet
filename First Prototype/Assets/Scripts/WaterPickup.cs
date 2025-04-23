using UnityEngine;

public class WaterPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D (Collider2D other) {
        if(other.gameObject.layer == 6) { // player layer
            float suckSpeed = 2 * Time.deltaTime; //other.transform.localScale.x / transform.localScale.x;
            GameManager.Instance.playerWater += transform.localScale.x * suckSpeed;
            transform.localScale -= transform.localScale * suckSpeed;
        }
        if(other.gameObject.layer == 4) { // water projectile layer
            transform.localScale += other.transform.localScale;
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.layer == 8) { // pickup layer
            if(transform.localScale.x > other.transform.localScale.x) {
                transform.localScale += other.transform.localScale;
                transform.position = (other.transform.position + transform.position) / 2;
                Destroy(other.gameObject);
            }
            
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
    }
}
