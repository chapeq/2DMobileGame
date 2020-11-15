using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string NPCname;
    private GameObject questToStart;
    public GameObject UIScreenBag;
    public GameObject UIScreenAni;
    public GameObject UIScreenBal;

    public void ChoixAction()
    {
        if (NPCname == "Hagrid")
        {
            questToStart = GameObject.Find("QuestAchats");
            questToStart.GetComponent<QuestController>().StartQuest();
        }
        else if (NPCname == "VendeurBaguette")
        {
            questToStart = GameObject.Find("QuestBaguette");
            questToStart.GetComponent<QuestController>().StartQuest();
            UIScreenBag.SetActive(true);
        }

        else if (NPCname == "VendeurAnimaux")
        {
            questToStart = GameObject.Find("QuestAnimaux");
            questToStart.GetComponent<QuestController>().StartQuest();
            UIScreenAni.SetActive(true);
        }
        else if (NPCname == "VendeurBalai")
        {
            questToStart = GameObject.Find("QuestBalai");
            questToStart.GetComponent<QuestController>().StartQuest();
            UIScreenBal.SetActive(true);
        }
    }

    public void SetNPCName(string npc)
    {
        NPCname = npc;
    }
}
