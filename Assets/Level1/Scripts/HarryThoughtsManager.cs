using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarryThoughtsManager : MonoBehaviour
{
    public static HarryThoughtsManager instance;
    public GameObject panel;
    public QuestController quest;
    public string text; 

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        panel.SetActive(false);

    }

    public void StartDialogue()
    {
        if (quest.isFinished)
        {
            panel.SetActive(true);
            panel.GetComponent<Text>().text = text;
            StartCoroutine("Affichage");
        }
    }

    IEnumerator Affichage()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(false);
    }
}
