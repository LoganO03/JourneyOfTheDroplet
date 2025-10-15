using UnityEngine;

public class LadderBottom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Ladder>().StopClimbing();
            Debug.Log("bot");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
