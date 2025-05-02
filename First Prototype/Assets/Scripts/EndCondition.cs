using System.Numerics;
using UnityEngine;

public class EndCondition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject player;

    public int scene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        endLevelOne();
        
    }

    public void endLevelOne(){
        if(scene == 1){
            if(player.transform.position.x >= 96 && player.transform.position.y <= -53){
            Rigidbody2D body = player.GetComponent<Rigidbody2D>();
            GameManager.Instance.canMove = false;
            body.AddForce(new UnityEngine.Vector2(15,10));
            GameManager.Instance.endLevel1 = true;
            Initiate.Fade("Stage2", Color.black, 1.0f);
            }
        }
    }
}
