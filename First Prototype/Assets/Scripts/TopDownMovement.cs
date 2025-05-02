using UnityEngine;

public class TopDownMovement : MonoBehaviour {

// The top down movement for the Intro Condensation Minigame
    public Camera cam;
    public float speed = 2f;
    void Update() {
        Vector2 input= Input.mousePosition;

        Vector3 worldInput = cam.ScreenToWorldPoint(input);

        Vector3 newPosition = Vector3.MoveTowards(transform.position, worldInput, speed * Time.deltaTime);

        newPosition.z = transform.position.z;

        transform.position = newPosition;
    }

}