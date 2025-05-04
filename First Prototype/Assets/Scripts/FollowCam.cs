using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public GameObject playerSprite;
    public float boundaryPercent;
    public float easing;
    public float scaleEasing;
    
    private Rigidbody2D rb2d;
    private Vector3 lastPosition;

    private float lBound;
    private float rBound;
    private float uBound;
    private float dBound;

    // Use this for initialization
    void Start()
    {
        rb2d = playerSprite.GetComponent<Rigidbody2D>();
        lastPosition = playerSprite.transform.position;
        lBound = boundaryPercent * Camera.main.pixelWidth;
        rBound = Camera.main.pixelWidth - lBound;
        dBound = boundaryPercent * Camera.main.pixelHeight;
        uBound = Camera.main.pixelHeight - dBound;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSprite && (playerSprite.transform.position != lastPosition || (rb2d.linearVelocity.x == 0 && rb2d.linearVelocity.y == 0)))
        {
            Vector3 deviance = Camera.main.WorldToViewportPoint(playerSprite.transform.position);
            //Debug.Log(deviance);
            if (Mathf.Abs(deviance.x - 0.5f) < 0.375f && Mathf.Abs(deviance.y - 0.5f) < 0.375f) {
                GetComponent<Camera>().orthographicSize += (Mathf.Max(playerSprite.transform.localScale.x * 8, 1) - GetComponent<Camera>().orthographicSize) * scaleEasing * Time.deltaTime;
                transform.localScale += (playerSprite.transform.localScale - transform.localScale) * scaleEasing * Time.deltaTime;
            } else {
                float scaleAmount = Mathf.Max(Mathf.Abs(deviance.x - 0.5f) + 1.125f, Mathf.Abs(deviance.x - 0.5f) + 1.125f);
                
                GetComponent<Camera>().orthographicSize += (GetComponent<Camera>().orthographicSize * scaleAmount - GetComponent<Camera>().orthographicSize) * scaleEasing * Time.deltaTime;
                transform.localScale += (transform.localScale * scaleAmount - transform.localScale) * scaleEasing * Time.deltaTime;
            }

            Vector3 spriteLoc = Camera.main.WorldToScreenPoint(playerSprite.transform.position);

            Vector3 pos = transform.position;

            if (spriteLoc.x < lBound)
            {
                pos.x -= lBound - spriteLoc.x;
            }
            else if (spriteLoc.x > rBound)
            {
                pos.x += spriteLoc.x - rBound;
            }

            if (spriteLoc.y < dBound)
            {
                pos.y -= dBound - spriteLoc.y;
            }
            else if (spriteLoc.y > uBound) {
                pos.y += spriteLoc.y - uBound;
            }
            pos = (Vector3.Lerp(transform.position, pos, easing) - transform.position) * Time.deltaTime;

            transform.position += pos;
        }
        lastPosition = playerSprite.transform.position;
    }
}
