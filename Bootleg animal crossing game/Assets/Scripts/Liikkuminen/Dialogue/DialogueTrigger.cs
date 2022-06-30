using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;



    public void NextSentance()
    {

        FindObjectOfType<DialogueManager>().DisplayNextSentence();
        
        
    }
    public void newDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    
}

