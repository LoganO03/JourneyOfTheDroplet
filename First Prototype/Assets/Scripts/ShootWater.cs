using UnityEngine;

public class ShootWater : MonoBehaviour
{
    public GameObject waterShot;

    private Rigidbody2D waterBody;
    private Rigidbody2D player;

    public int moveForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        //waterBody = waterShot.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButton(0))
        {
            Debug.Log("Shooting");
            Shoot();
        }
        
    }

    void Shoot(){
        Vector2 spawn = player.transform.position;
        Vector2 goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject water = Instantiate(waterShot, spawn, Quaternion.identity);
        Debug.Log("Adding Force: " + (goal * moveForce));
        waterBody = water.GetComponent<Rigidbody2D>();
        waterBody.AddForce(goal * moveForce);
        Debug.Log("Velocity: " + waterBody.linearVelocity);

    }
}
