using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string NPCname;
    public GameObject UIScreenBag;
    public GameObject UIScreenAni;
    public GameObject UIScreenBal;

    private GameObject questToStart;

    public void ChoixAction()
    {
        switch (NPCname)
        {
            case "Hagrid":
                questToStart = GameObject.Find("QuestAchats");
                questToStart.GetComponent<QuestController>().StartQuest();
                if (questToStart.GetComponent<QuestController>().isFinished)
                {
                    LevelChanger.instance.FadeToNextLevel();
                }
                 
                break;

            case "VendeurBaguette":
                if (UIScreenBag != null)
                    UIScreenBag.SetActive(true);
                break;

            case "VendeurAnimaux":
                if (UIScreenAni != null)
                    UIScreenAni.SetActive(true);
                break;

            case "VendeurBalai":
                if (UIScreenBal != null)
                    UIScreenBal.SetActive(true);
                break;

            default:
                break;
        }

    }

    public void SetNPCName(string npc)
    {
        NPCname = npc;
    }
}
