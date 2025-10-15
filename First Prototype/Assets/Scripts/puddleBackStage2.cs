using UnityEngine;

public class puddleBackStage2 : MonoBehaviour
{
    [SerializeField]
    Puddle water;

    bool collidingWithPuddle = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (collidingWithPuddle == false)
        {
            water.AddWaterOverTime(0.005f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            collidingWithPuddle = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            collidingWithPuddle = false;
        }
    }
}
