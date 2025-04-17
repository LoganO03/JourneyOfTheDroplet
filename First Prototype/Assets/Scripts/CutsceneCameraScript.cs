// Borrowed from Jauss and Leigh's Belonging (Thank you so much friends)
using UnityEngine;

public class CutsceneCameraScriptY : MonoBehaviour
{
    public float destinationY;       // Target Y position
    public float slurpSpeed;         // How quickly the camera accelerates
    public float maxSpeed;           // Maximum camera speed

    public GameObject background0;   // Background object to move with parallax
    public float speed0;             // Background parallax speed factor

    float curSlurp;                  // Internal smoothing factor

    void Start()
    {
        curSlurp = 0;
    }

    void Update()
    {
        curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
        float cameraSpeed = Mathf.Min(maxSpeed, (destinationY - this.transform.position.y) * curSlurp) * Time.deltaTime;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + cameraSpeed, this.transform.position.z);
        background0.transform.position = new Vector3(background0.transform.position.x,background0.transform.position.y + cameraSpeed * speed0, background0.transform.position.z);
    }
}
