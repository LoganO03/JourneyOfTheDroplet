using UnityEngine;

public class Erode : MonoBehaviour
{
    public GameObject water;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //"Wall" name will need to be changed to actual name of layer
        if (col.gameObject.tag == "Wall")
        {
            col.gameObject.SetActive(false);
            Destroy(col.gameObject, 1);
        }
        //add if statement to know if x or y should shrink (Optional)
        //water.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        water.SetActive(false);
    }
}
