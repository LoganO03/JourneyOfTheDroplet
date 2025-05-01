using UnityEngine;

public class puddleBack : MonoBehaviour
{
    
    [SerializeField] Puddle water;
    
    bool collidingWithPuddle = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D (Collider2D other) {
        if(other.gameObject.name == "water") { // pickup layer
            collidingWithPuddle = true;
        }
        else if(collidingWithPuddle && (other.gameObject.layer == 4 || other.gameObject.layer == 8)) { 
            water.getFilld(other);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.name == "water") { // pickup layer
            collidingWithPuddle = false;
        }
    }
}
