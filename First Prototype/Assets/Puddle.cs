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
            float suckSpeed = 0.25f * Time.deltaTime * other.transform.localScale.x; //other.transform.localScale.x / transform.localScale.x;
            float suckWater = (transform.localScale.x * transform.localScale.y) * suckSpeed;
            GameManager.Instance.playerWater += suckWater;
            transform.localPosition -= Vector3.up * suckWater / transform.localScale.z;
        }
        if(other.gameObject.layer == 8) { // pickup layer
            float suckSpeed = 0.25f * Time.deltaTime / other.transform.localScale.z; //other.transform.localScale.x / transform.localScale.x;
            float suckWater = (transform.localScale.x * transform.localScale.y * transform.localScale.z) * suckSpeed;
            transform.localPosition += Vector3.up * Mathf.Min(suckWater + transform.localPosition.y, fullHeight.y);
            other.transform.localScale -= new Vector3(other.transform.localScale.x, other.transform.localScale.y, 0) * (suckWater / (other.transform.localScale.x * other.transform.localScale.y * other.transform.localScale.z));
        }
        if(other.gameObject.layer == 4) { // water projectile layer
            float otherVolume = (Mathf.Pow(other.transform.localScale.x, 3) * 4 * Mathf.PI) / 3;
            transform.localPosition += Vector3.up * otherVolume / (transform.localScale.x * transform.localScale.y * transform.localScale.z);
        }
    }

    public void getFilld (Collider2D other) {
        if(other.gameObject.layer == 8) { // pickup layer
            float suckSpeed = 0.25f * Time.deltaTime / other.transform.localScale.z; //other.transform.localScale.x / transform.localScale.x;
            float suckWater = (transform.localScale.x * transform.localScale.y * transform.localScale.z) * suckSpeed;
            transform.localPosition += Vector3.up * (Mathf.Min(suckWater + (fullHeight.y - transform.localPosition.y), fullHeight.y) - transform.localPosition.y);
            other.transform.localScale -= Vector3.one * (suckWater / (other.transform.localScale.x * other.transform.localScale.y * other.transform.localScale.z));
        }
        if(other.gameObject.layer == 4) { // water projectile layer
            float otherVolume = (Mathf.Pow(other.transform.localScale.x, 3) * 4 * Mathf.PI) / 3;
            transform.localPosition += Vector3.up * otherVolume / (transform.localScale.x * transform.localScale.y * transform.localScale.z);
        }
    }
}
