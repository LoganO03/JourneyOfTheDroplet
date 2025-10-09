using UnityEngine;

public class JumpPad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //tells the player it is on the jumppad
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponent<PlayerMovement>().ToggleBigJump(true);
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponent<PlayerMovement>().ToggleBigJump(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
