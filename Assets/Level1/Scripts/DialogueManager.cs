using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;
    public Text TexteVisu;
    public Animator animator;
    private PlayerController playerMove;


    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        animator.SetBool("isOpen", false);
        playerMove = GameObject.Find("PlayerHarry").GetComponent<PlayerController>();
    }

    public void ShowDialogue(string dialogue)
    {
        animator.SetBool("isOpen", true);
        playerMove.canMove = false;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue));
    } 

    public void EndDialogue()
    {
        AudioManager.instance.Stop("TypeWriter");
        animator.SetBool("isOpen", false);
        playerMove.canMove = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        AudioManager.instance.Play("TypeWriter");
        TexteVisu.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(0.04f);
            TexteVisu.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        AudioManager.instance.Stop("TypeWriter");
    }
}
