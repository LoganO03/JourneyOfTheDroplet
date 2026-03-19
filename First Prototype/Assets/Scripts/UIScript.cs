using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{

    public GameObject panel;
    public TextMeshProUGUI dialoguetext;
    public TextMeshProUGUI nametext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       GameManager.Instance.dialoguePanel = panel;
       GameManager.Instance.dialogueText = dialoguetext; 
       GameManager.Instance.nameText = nametext; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
