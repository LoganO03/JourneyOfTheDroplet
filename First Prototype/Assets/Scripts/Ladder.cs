using System.Collections;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    bool isClimbable = false;
    bool isClimbing = false;
    GameObject top;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        top = transform.Find("TopLadder").gameObject;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isClimbable = true;
            Debug.Log("can climb");
            StartCoroutine(canClimb(col.gameObject));
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isClimbable = false;
            isClimbing = false;
            top.GetComponent<Collider2D>().enabled = true;
        }
    }

    public void StopClimbing()
    {
        isClimbing = false;
    }
    private IEnumerator canClimb(GameObject player)
    {
        while (isClimbable)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                Debug.Log("start");
                player.GetComponent<PlayerMovement>().StartClimb(transform.position.x);
                isClimbing = true;
                isClimbable = false;
                top.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                yield return null;
            }

        }
        while (isClimbing)
        {
            yield return null;
        }
        player.GetComponent<PlayerMovement>().EndClimb();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
