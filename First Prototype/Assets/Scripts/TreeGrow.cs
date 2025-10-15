using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
    GameObject TreeTrunk;
    SpriteRenderer topSR;
    SpriteRenderer trunkSR;
    GameObject TreeTop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TreeTrunk = transform.Find("TreeBase").gameObject;
        TreeTop = transform.Find("TreeTop").gameObject;
        topSR = TreeTop.GetComponent<SpriteRenderer>();
        trunkSR = TreeTrunk.GetComponent<SpriteRenderer>();
    }

    public void GrowTree(float growthrate, float maxw, float maxh)
    {
        if (topSR.size.x < maxw)
        {
            topSR.size += new Vector2(growthrate, 0);
        }
        else if (topSR.size.x > maxw)
        {
            topSR.size = new Vector2(maxw, topSR.size.y);
        }

        if (trunkSR.size.y < maxh)
        {
            trunkSR.size += new Vector2(0, growthrate);
            TreeTop.transform.position += new Vector3(0, growthrate * 0.45f, TreeTop.transform.position.z);
        }
        else if (trunkSR.size.y > maxh)
        {
            trunkSR.size = new Vector2(trunkSR.size.x, maxh);
        }
        if (trunkSR.size.y == maxh && topSR.size.x == maxw)
        {
            TreeTop.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
