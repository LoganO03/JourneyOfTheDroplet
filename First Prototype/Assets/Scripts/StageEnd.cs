using System.Collections;
using UnityEngine;

public class StageEnd : MonoBehaviour
{

    public GameObject maincamera;
    public int scene;
    private bool ending;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            endStage(collision.gameObject);
            maincamera.GetComponent<FollowCam>().enabled = false;
            
        }
    }

    IEnumerator SwimAway(GameObject player)
    {
        yield return new WaitForSeconds(4f);
        //GameManager.Instance.endLevel1 = true;
        //Initiate.Fade("Stage2", Color.black, 1.0f);
    }

    public void endStage(GameObject player)
    {
        Debug.Log("end");
        if(scene == 1){
            Rigidbody2D body = player.GetComponent<Rigidbody2D>();
            player.GetComponent<PlayerMovement>().StartSwim();
            GameManager.Instance.StopMove();
            body.mass = 0;
            body.gravityScale = 0;
            
            //StartCoroutine(SwimAway(player));
        }
    }

}


