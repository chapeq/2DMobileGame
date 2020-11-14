using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string NPCname;
    private GameObject quest;
    public GameObject UIScreenBag;
    public GameObject UIScreenAni;
    public GameObject UIScreenBal;

    public void ChoixAction()
    {
        if (NPCname == "Hagrid")
        {
            quest = GameObject.Find("Quest");
            quest.GetComponent<QuestController>().StartQuest();
        }
        else if (NPCname == "VendeurBaguette")
        {
            UIScreenBag.SetActive(true);
        }

        else if (NPCname == "VendeurAnimaux")
        {
            UIScreenAni.SetActive(true);
        }
        else if (NPCname == "VendeurBalai")
        {
            UIScreenBal.SetActive(true);
        }
    }

    public void SetNPCName(string npc)
    {
        NPCname = npc;
    }
}
