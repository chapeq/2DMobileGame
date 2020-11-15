using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public static DialogManager instance;
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

    public void StartDialogue(Dialog dialogue)
    {
        animator.SetBool("isOpen", true);
        IsDialogOn = true;
        TexteVisu.text = dialogue.texte;
    } 

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        IsDialogOn = false;
    }
}
