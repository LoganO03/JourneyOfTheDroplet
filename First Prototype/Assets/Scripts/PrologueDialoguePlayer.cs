using UnityEngine;
using TMPro;

public class PrologueDialoguePlayer : MonoBehaviour
{
    [Header("References")]
    public DialogueAsset dialogueAsset;
    public TextMeshProUGUI dialogueText;
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
    }

    void Update()
    {
        if (!dialogueActive)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        Initiate.Fade("IntroLeadIn", Color.black, 1.0f);
    }
}