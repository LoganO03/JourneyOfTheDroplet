using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabler : MonoBehaviour
{
    public GameObject twoButton;
    public GameObject threeButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        if(GameManager.Instance != null){
        twoButton.GetComponent<Button>().interactable = GameManager.Instance.endLevel1;
        threeButton.GetComponent<Button>().interactable = GameManager.Instance.endLevel2;
        }
        else
        {
            twoButton.GetComponent<Button>().interactable = false;
            threeButton.GetComponent<Button>().interactable = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
