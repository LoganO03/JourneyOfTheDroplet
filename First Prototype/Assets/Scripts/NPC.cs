
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] public bool firstInteraction = true;
    [SerializeField] int repeatStartPosition;

    public bool AutoTalk;
    public string npcName;
    public DialogueAsset dialogueAsset;

    [HideInInspector]
    public int StartPosition {
        get
        {
            if (firstInteraction)
            {
                firstInteraction = false;
                return 0;
            }
            else
            {
                return repeatStartPosition;
            }
        }
    }
    public bool firstConvo(){
        return firstInteraction;
    }
    public bool notSpokenTo()
    {
        if (AutoTalk)
        {
            return firstInteraction;    
        }
        else
        {
            return false;
        }
    }
}
