using UnityEngine;

public class StalactiteDrip : MonoBehaviour
{


    
    //have existing objects in a queue instead of creating

   
    public GameObject waterDrop;

    private Rigidbody2D stalactite;



    public bool canDrip = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
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
        waterDrop.SetActive(true);
        Vector2 spawn = stalactite.transform.position;
       // GameObject water = Instantiate(waterShot, spawn, Quaternion.identity);
        //Debug.Log("Adding Force: " + ((goal - spawn) * moveForce));
        waterDrop.transform.position = spawn;
        
        canDrip = false;
        

        
        

    }
}
