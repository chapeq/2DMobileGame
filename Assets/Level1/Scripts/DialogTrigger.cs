using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialogue;

   // private bool displaytext = true;
    public QuestController quest;

    public ScriptButton bouton; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bouton.SetNPCName(gameObject.name);
            if (quest != null)
            {
                if (!quest.isStarted)
                    DialogManager.instance.StartDialogue(dialogue);
                else if (quest.IsQuestComplete())
                    DialogManager.instance.StartDialogue(quest.questCompletedConversation);
                else
                    DialogManager.instance.StartDialogue(quest.questInProgressConversation);       
            } 
            else
               DialogManager.instance.StartDialogue(dialogue);

           // displaytext = false;
        }
    }

}
