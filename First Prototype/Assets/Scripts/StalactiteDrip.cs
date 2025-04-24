using UnityEngine;

public class StalactiteDrip : MonoBehaviour
{


    
    //have existing objects in a queue instead of creating

   
    public GameObject waterDrop;

    private Rigidbody2D stalactite;
    private Vector3 startSize;


    public bool canDrip = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        startSize = transform.localScale;
        waterDrop.SetActive(false);
        stalactite = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canDrip){
            Drip();
        }
        
        
    }

    void Drip(){

        Vector2 spawn = stalactite.transform.position;
        waterDrop.transform.position = spawn;

        waterDrop.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0, 0, 0);
        transform.localScale = startSize;

        waterDrop.SetActive(true);
        
        canDrip = false;
    }
}
