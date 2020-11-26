using UnityEngine;

public class ScriptButtonLevel1 : MonoBehaviour
{
   
    public string NPCname;
    public GameObject UIScreenBag;
    public GameObject UIScreenAni;
    public GameObject UIScreenBal;

    public GameObject UISpells;
    public GameObject UISpells2;

    public GameObject UIPotion;
    public GameObject PotionManager;

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

            case "TriggerMcGo":
                if (UISpells != null)
                {
                    UISpells.SetActive(true);
                    PlayerStats.instance.AddPtsDefense(10);
                }
                break;

            case "ValidateProtego":
                if (UISpells2 != null)
                {
                    UISpells2.SetActive(true);
                    PlayerStats.instance.AddPtsAttaque(10);
                }
                break;

            case "ButtonManager1":
                if (UIPotion != null)
                    UIPotion.SetActive(true);
                if (PotionManager != null)
                    PotionManager.SetActive(true);
                break;

            case "Centaure":
                LevelChanger.instance.FadeToNextLevel();
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
