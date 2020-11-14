using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{

    public string titre;
    public string desc;
    public Dialog questInProgressConversation, questCompletedConversation;


    private HashSet<string> inv  = new HashSet<string>();

    [System.Serializable]
    public class ItemRequirement
    {
        public Item item;
        public int count = 1;
    }

    public ItemRequirement[] requiredItems;

    public bool isStarted = false;
  

    public void StartQuest()
    {
        if(!isStarted)
            isStarted = true;    
    }

    public bool IsQuestComplete()
    {
        foreach (var i in requiredItems)
        {
            if (inv.Contains(i.item.name))
            return true;
        }
        return false;
    }


}
