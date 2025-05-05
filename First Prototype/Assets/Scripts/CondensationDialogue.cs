using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CondensationDialogue : MonoBehaviour
{
    [Header("References")]
    public DialogueAsset dialogueAsset;
    public TextMeshProUGUI dialogueText;
    public CanvasGroup dialogueCanvasGroup;

    [Header("Timing")]
    public float startDelay = 2f;
    public float displayDuration = 4f;
    public float fadeDuration = 1f;

    private int currentLine = 0;

    void Start()
    {
        if (dialogueAsset == null || dialogueText == null || dialogueCanvasGroup == null)
        {
            Debug.LogWarning("CondensationDialogue: Missing references.");
            return;
        }

        dialogueCanvasGroup.alpha = 1f; // Ensure it's hidden at start
        StartCoroutine(PlayDialogueSequence());
    }

    IEnumerator PlayDialogueSequence()
    {
        yield return new WaitForSeconds(startDelay);

        dialogueCanvasGroup.gameObject.SetActive(true);

        while (currentLine < dialogueAsset.dialogue.Length)
        {
            dialogueText.text = dialogueAsset.dialogue[currentLine];

            // Fade in
            yield return StartCoroutine(FadeCanvasGroup(dialogueCanvasGroup, 0f, 1f));

            yield return new WaitForSeconds(displayDuration);

            // Fade out
            yield return StartCoroutine(FadeCanvasGroup(dialogueCanvasGroup, 1f, 0f));

            currentLine++;
        }

        EndDialogue();
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            cg.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cg.alpha = to;
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        dialogueCanvasGroup.gameObject.SetActive(false);
        Debug.Log("Section ended.");
    }
}