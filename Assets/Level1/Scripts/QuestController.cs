using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{

    public string titre;
    public string desc;
    public Dialogue questInProgressConversation, questCompletedConversation;

    EquipementInventory inv;
    HarryThoughtsManager indicefin;


    [System.Serializable]
    public class ItemRequirement
    {
        public Item item;
        public int count = 1;
    }

    public ItemRequirement[] requiredItems;

    public bool isStarted = false;
    public bool isFinished = false;

    private void Start()
    {
        inv = EquipementInventory.instance;
        indicefin = HarryThoughtsManager.instance;
    }

    public void StartQuest()
    {
        if(!isStarted)
            isStarted = true;    
    }

    public bool IsQuestComplete()
    {
        if (isFinished)
            return true;
        foreach (var i in requiredItems)
        {

            if (!inv.equipements.Exists(x => x.type == i.item.type))
                return false;
        }
        isFinished = true;
        return true;
        
    }


}
