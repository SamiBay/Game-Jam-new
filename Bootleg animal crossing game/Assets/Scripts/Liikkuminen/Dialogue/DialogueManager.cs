using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{


    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;

    public Animator anim;
    bool displaynext;
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue (Dialogue dialogue)
    {
        anim.SetBool("isOpen", true);
        
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {   
        if (sentences.Count == 0)
        {       
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {   
        anim.SetBool("isOpen", false);
        FindObjectOfType<NavMeshMovement>().SpeedNormalized();
        FindObjectOfType<WayPointMover>().DialogueEnd(); 
        FindObjectOfType<WayPointMover1Froggy>().DialogueEnd();
        FindObjectOfType<WayPointMoverastronomymoth>().DialogueEnd();
        FindObjectOfType<WayPointMoverPostDog>().DialogueEnd();
        FindObjectOfType<WayPointMoverLibraryMoth>().DialogueEnd();
        
    }
}   
