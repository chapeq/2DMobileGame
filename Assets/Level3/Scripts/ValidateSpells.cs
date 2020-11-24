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
        Destroy(panel);
        GetComponent<DialogTrigger>().DisplayOnclick();
        if (triggerMcgo != null)
        {
            Destroy(triggerMcgo);
            TriggerLevel.SetActive(true);
        }
        
    }

    public void Fail()
    {
        if (!isFinished)
        {
            circle.Reset();
            foreach (Checkpoint point in listPoints)
            {
                point.Reset();
            }
            Debug.Log("Plus vite !! ou plus precis ! ");
        }
    }

}
