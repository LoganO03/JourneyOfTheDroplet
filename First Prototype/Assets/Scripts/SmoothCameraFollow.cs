using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float scaleSpeed = 2f;
    public Camera cam;

    public float startZoom = 50f;       // Starting zoom level
    public float minZoom = 5f;          // Final zoom level
    public float zoomDelay = 2f;        // How long to wait before starting the zoom

    private bool isZooming = false;
    private float delayTimer = 0f;

    void Start()
    {
        if (cam != null)
        {
            cam.orthographicSize = startZoom;
        }
    }

    void Update()
    {
        // Smooth follow
        Vector3 positionLerp = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        positionLerp.z = transform.position.z;
        transform.position = positionLerp;

        // Handle zoom delay
        if (!isZooming)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= zoomDelay)
            {
                isZooming = true;
            }
        }

        // Zoom in after delay
        if (isZooming)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, minZoom * target.localScale.x, scaleSpeed * Time.deltaTime);

            if (Mathf.Abs(cam.orthographicSize - minZoom * target.localScale.x) < 0.05f)
            {
                cam.orthographicSize = minZoom * target.localScale.x;
                isZooming = false; // Optional: remove if you want it to keep tracking scale changes
            }
        }
    }
}