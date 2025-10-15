using System.Collections;
using UnityEngine;
using TMPro; 

public class DialoguePlayer : MonoBehaviour
{
    [Header("References")]
    public DialogueAsset dialogueAsset;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI continueHint;
    private bool hintSkipped = false;
    private int currentLine = 0;
    private bool dialogueActive = false;


    void Start()
    {
        if (dialogueAsset == null || dialogueText == null)
        {
            Debug.LogWarning("DialoguePlayer: Missing references.");
            return;
        }
        dialogueActive = true;
        currentLine = 0;
        ShowCurrentLine();
        StartCoroutine(EToContinue());
    }
    void Update()
    {
        if (!dialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hintSkipped)
            {
                hintSkipped = true;
            }
            NextLine();
        }
    }

    void ShowCurrentLine()
    {
        if (currentLine < dialogueAsset.dialogue.Length)
        {
            dialogueText.text = dialogueAsset.dialogue[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    void NextLine()
    {
        currentLine++;
        ShowCurrentLine();
    }

    void EndDialogue()
    {
        dialogueActive = false;
        dialogueText.text = "";
        Debug.Log("Section ended.");
        Initiate.Fade("Cutscene1Finish", Color.black, 1.0f);
    }

    IEnumerator EToContinue()
    {
        yield return new WaitForSeconds(5);
        continueHint.gameObject.SetActive(true);
        float time = 0;
        Color startC = new Color(1, 1, 1, 0);
        Color endC = new Color(1, 1, 1, 1);
        while (time < 2f)
        {
            continueHint.color = Color.Lerp(startC, endC, time / 2);
            time += Time.deltaTime;
            yield return null;
        }
        continueHint.color = endC;

        while (!hintSkipped)
        {
            yield return null;
            
        }
        continueHint.gameObject.SetActive(false);
    }
}
