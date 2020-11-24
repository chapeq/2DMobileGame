using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public QuestController quest;
    public ScriptButton bouton;

    public bool hasMet = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("DialogTrigger _ OnTriggerEnter");
            if (bouton!=null)
            bouton.SetNPCName(gameObject.name);

            if (quest != null)
            {
                if (!quest.isStarted)
                    DialogueManager.instance.ShowDialogue(dialogue.texte[0]);
                else if (quest.IsQuestComplete())
                    DialogueManager.instance.ShowDialogue(quest.questCompletedConversation.texte[0]);
                else
                    DialogueManager.instance.ShowDialogue(quest.questInProgressConversation.texte[0]);
            }
            else
            {
                if (!hasMet)
                {
                    DialogueManager.instance.ShowDialogue(dialogue.texte[0]);
                    hasMet = true;
                }
                else
                    DialogueManager.instance.ShowDialogue(dialogue.texte[dialogue.texte.Length -1]);
            }
         
        }
    }

    public void DisplayOnclick()
    {
        Debug.Log("DialogTrigger _ displayOnclick");
        if (bouton != null)
            bouton.SetNPCName(gameObject.name);
        if (!hasMet)
        {
            DialogueManager.instance.ShowDialogue(dialogue.texte[0]);
            hasMet = true;
        }
        else
            DialogueManager.instance.ShowDialogue(dialogue.texte[dialogue.texte.Length - 1]);
    }

}
