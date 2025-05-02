using UnityEngine;

public class WellEntryCondition : MonoBehaviour
{

    public GameObject blockage;
    public NPC well;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!well.firstInteraction){
            blockage.SetActive(false);
        }
        
    }
}
