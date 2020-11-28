using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateSpells : MonoBehaviour
{
    public GameObject panel; 
    public DragOnTouch circle;
    public Checkpoint[] listPoints;
    public GameObject triggerMcgo;
    public GameObject TriggerLevel;
    bool isFinished = false;

    public void Finish()
    {
        Debug.Log("FINISH");
        isFinished = true;
        AudioManager.instance.Play("Spell");
        StartCoroutine(HidePanel());      
    }

    public void Fail()
    {
        if (!isFinished)
        {
            AudioManager.instance.Play("TimerEnd");
            circle.Reset();
            foreach (Checkpoint point in listPoints)
            {
                point.Reset();
            }
            Debug.Log("Plus vite !! ou plus precis ! ");
        }
    }

    IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(2f);
        Destroy(panel);
        GetComponent<DialogTrigger>().DisplayOnclick();
        if (triggerMcgo != null)
        {
            Destroy(triggerMcgo);
            TriggerLevel.SetActive(true);
        }

    }

}
