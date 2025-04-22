using UnityEngine;

public class StalactiteDrip : MonoBehaviour
{


    
    //have existing objects in a queue instead of creating

   
    public GameObject waterDrop;


    private GameObject wd;

    private Rigidbody2D stalactite;

    private StalactiteWater sw;




    public bool canDrip = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
        //waterDrop.SetActive(false);
        stalactite = GetComponent<Rigidbody2D>();
        //sw = GetComponentInChildren<StalactiteWater>();
        
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
        wd = Instantiate(waterDrop, spawn, Quaternion.identity);
        wd.SetActive(true);
        sw = wd.GetComponentInChildren<StalactiteWater>();
        sw.water = wd;
        canDrip = false;
        //Debug.Log("" + canDrip);
        

        
        

    }
}
