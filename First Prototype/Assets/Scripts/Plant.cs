using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float growthRate = 0.1f;
    public float speed = 1;
    public bool ladder;
    public bool bounce;
    public bool platform;
    public bool tree;
    public float maxWidth;
    public float maxHeight;

    bool isClimbable = false;
    

    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    //col.gameObject.GetComponent<PlayerMovement>().StartClimb();

    public void Grow()
    {
        if (ladder)
        {
            sr.size += new Vector2(0, growthRate);
            if (sr.size.y >= maxHeight)
            {
                sr.size = new Vector2(sr.size.x, maxHeight);
                GetComponent<Collider2D>().isTrigger = true;
                GetComponent<Ladder>().enabled = true;
                transform.Find("TopLadder").position = new Vector3(transform.position.x, (transform.position.y + maxHeight) - 0.1f, 1);
            }

        }
        else if (bounce)
        {
            //when mushroom is right width, jump pad is enabled
            if (transform.localScale.x >= maxWidth)
            {
                transform.localScale = new Vector2(maxWidth, transform.localScale.y);
                    GetComponent<JumpPad>().enabled = true;
                    GetComponent<BoxCollider2D>().enabled = true;
                

            }
            else if (transform.localScale.y >= maxHeight)
            {
                transform.localScale = new Vector3(transform.localScale.x + growthRate, maxHeight, 1);
            }
            else
            {
                transform.localScale += new Vector3(growthRate, growthRate, 0);
                transform.position += new Vector3(0, -(growthRate / 3), 0);
                
                
            }
        }
        else if (tree)
        {
            // it is a tree
            if (transform.localScale.x >= 1 || transform.localScale.y >= 1)
            {
                if (GetComponent<BoxCollider2D>().excludeLayers == LayerMask.GetMask("Nothing"))
                {
                    GetComponent<BoxCollider2D>().excludeLayers = LayerMask.GetMask("Player");
                }
                transform.localScale = new Vector2(1, 1);
                GetComponent<TreeGrow>().GrowTree(growthRate, maxWidth, maxHeight);
            }
            else
            {
                transform.localScale += new Vector3(growthRate, growthRate, 0);
                transform.position += new Vector3(0, growthRate, 0);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
