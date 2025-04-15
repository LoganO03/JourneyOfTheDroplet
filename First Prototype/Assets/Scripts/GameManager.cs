//Borrowed from Dr. Goadrich's code for "Townies"
using System.Collections;
using TMPro;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager Instance { get; private set; }


    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject CharacterSprite;

    [SerializeField] (Vector3, Vector3) CharacterPortrait;

    

    // delay typing text until after the panel has moved into place
    [SerializeField] float DialoguePanelDelay; 

    // target Lerp speed for the dialogue panel
    [SerializeField] float slurpSpeed; 

    // Desired position for the dialogue panel while on
    [SerializeField] Vector3 DialogueOn; 
    
    // Desired position for the dialogue panel while off
    [SerializeField] Vector3 DialogueOff; 


    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    public Vector2 playerFacing;
    bool skipLineTriggered;
    float delay;
    float curSlurp;
    (Vector3, Vector3) Porigin; // portrait origin ;3

    public void StartDialogue(string[] dialogue, int startPosition, string name, Sprite spritename, (Vector3, Vector3) portrait)
    {
        nameText.text = name + "...";
        CharacterSprite.GetComponent<SpriteRenderer>().sprite = spritename;
        delay = DialoguePanelDelay;
        CharacterPortrait = portrait;
        Porigin = (CharacterSprite.transform.localPosition, CharacterSprite.transform.localScale);
        StopAllCoroutines();
        StartCoroutine(DeployDialogue());
        StartCoroutine(RunDialogue(dialogue, startPosition));
    }

    IEnumerator RunDialogue(string[] dialogue, int startPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();
        
        for (int i = startPosition; i < dialogue.Length; i++)
        {
            //dialogueText.text = dialogue[i];
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }
        OnDialogueEnded?.Invoke();
        EndDialogue();
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    // depricated?
    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        StopAllCoroutines();
        StartCoroutine(UndeployDialogue());
    }

    float charactersPerSecond = 90;

    IEnumerator TypeTextUncapped(string line)
    {
        float timer = delay;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;

        while (i < chars.Length) {
            if (timer < Time.deltaTime) {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            } else {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
        delay = 0;
    }

    IEnumerator DeployDialogue() {
        while (dialoguePanel.transform.localPosition != DialogueOn) {
            curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
            dialoguePanel.transform.localPosition = Vector3.Lerp(dialoguePanel.transform.localPosition, DialogueOn, curSlurp);
            CharacterSprite.transform.localPosition = Vector3.Lerp(CharacterSprite.transform.localPosition, CharacterPortrait.Item1, curSlurp);
            CharacterSprite.transform.localScale = Vector3.Lerp(CharacterSprite.transform.localScale, CharacterPortrait.Item2, curSlurp);
            yield return null;
        }
        curSlurp = 0;
    }

    IEnumerator UndeployDialogue() {
        while (dialoguePanel.transform.localPosition != DialogueOff) {
            curSlurp = curSlurp + (slurpSpeed - curSlurp) * slurpSpeed;
            dialoguePanel.transform.localPosition = Vector3.Lerp(dialoguePanel.transform.localPosition, DialogueOff, curSlurp);
            CharacterSprite.transform.localPosition = Vector3.Lerp(CharacterSprite.transform.localPosition, Porigin.Item1, curSlurp);
            CharacterSprite.transform.localScale = Vector3.Lerp(CharacterSprite.transform.localScale, Porigin.Item2, curSlurp);
            yield return null;
        }
        curSlurp = 0;
    }
}