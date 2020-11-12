using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public static DialogManager instance;
    public Text TextArea;
    public Animator animator;

    private Queue<string> sentences;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        sentences = new Queue<string>();
        animator.SetBool("isOpen", false);
    }

    public void StartDialogue(Dialog dialogue)
    {
        animator.SetBool("isOpen", true);
        sentences.Clear();  

        foreach(string sentence in dialogue.texte)
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
        Debug.Log("Next sentence :" + sentence);
        TextArea.text = sentence;

    }

    void EndDialogue()
    {
        Debug.Log("Fermeeeeeyyy");
        animator.SetBool("isOpen", false);

    }
}
