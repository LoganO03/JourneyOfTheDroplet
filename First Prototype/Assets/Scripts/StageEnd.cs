using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StageEnd : MonoBehaviour
{

    public GameObject maincamera;
    public int scene;
    public GameObject endpanel;
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
            if(scene == 1)
            {
                maincamera.GetComponent<FollowCam>().enabled = false;    
            }
            

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(scene == 1){
                GameManager.Instance.endLevel1 = true;
                Initiate.Fade("Stage2", Color.black, 1.0f);
                GameManager.Instance.canMove = true;
                GameManager.Instance.SetWater(10);
            }
        }
    }



    public void endStage(GameObject player)
    {
        Debug.Log("end");
        if(scene == 1){
            Rigidbody2D body = player.GetComponent<Rigidbody2D>();
            player.GetComponent<PlayerMovement>().StartSwim();
            GameManager.Instance.endLevel1 = true;
            body.mass = 0;
            body.gravityScale = 0;
        }
        if(scene == 2){
            GameManager.Instance.endLevel2 = true;
            endpanel.SetActive(true);
            StartCoroutine(Ending());

        }
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(4f);
        endpanel.SetActive(false);
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }

}


