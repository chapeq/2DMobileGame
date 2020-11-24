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
    public GameObject UISpells;
    public GameObject UISpells2;
    public GameObject UIPotion;
    public GameObject PotionManager;

    private GameObject questToStart;
    private GameObject harry;

    public void ChoixAction()
    {
        Debug.Log("ChoixAction :" + NPCname);
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

            case "TriggerMcGo":
                if (UISpells != null)
                {
                    UISpells.SetActive(true);
                    harry = GameObject.Find("PlayerHarry");
                    harry.GetComponent<PlayerStats>().AddPtsDefense(10);
                }
                break;

            case "ValidateProtego":
                if (UISpells2 != null)
                {
                    UISpells2.SetActive(true);
                    harry = GameObject.Find("PlayerHarry");
                    harry.GetComponent<PlayerStats>().AddPtsAttaque(10);
                }
                break;

            case "ButtonManager1":
                if (UIPotion != null)
                    UIPotion.SetActive(true);
                if (PotionManager != null)
                    PotionManager.SetActive(true);
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
