using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;
    public Text TexteVisu;
    public Animator animator;
    public bool IsDialogOn;


    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        animator.SetBool("isOpen", false);
        IsDialogOn = false;
    }

    public void ShowDialogue(string dialogue)
    {
        animator.SetBool("isOpen", true);
        IsDialogOn = true;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue));
    } 

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        IsDialogOn = false;
    }

    IEnumerator TypeSentence(string sentence)
    { 
        TexteVisu.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(0.04f);
            TexteVisu.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
