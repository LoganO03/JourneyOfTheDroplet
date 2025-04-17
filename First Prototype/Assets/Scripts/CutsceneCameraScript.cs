// Borrowed from Jauss and Leigh's Belonging (Thank you so much friends)
using UnityEngine;

public class CutsceneCameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float destination;
    public float slurpSpeed;
    public float maxSpeed;
    

    public GameObject background0;
    public float speed0;

    float curSlurp;


    void Start()
    {
        curSlurp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
        float cameraSpeed = Mathf.Min(maxSpeed, (destination - this.transform.position.y) * curSlurp) * Time.deltaTime;
        this.transform.position = new Vector3(this.transform.position.x + cameraSpeed, this.transform.position.y, this.transform.position.z);
        background0.transform.position = new Vector3(background0.transform.position.x + cameraSpeed * speed0, background0.transform.position.y, background0.transform.position.z);
    }
}