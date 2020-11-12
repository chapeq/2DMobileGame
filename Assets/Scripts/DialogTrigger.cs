using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialogue;

    public bool isInRange = false;

    private void Update()
    {
        if (isInRange)
            TriggerDialogue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isInRange = false;

    }

    private void TriggerDialogue()
    {
       DialogManager.instance.StartDialogue(dialogue);
    }
}
