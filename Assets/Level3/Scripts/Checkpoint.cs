using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameObject nextCheckpoint; 
    public ValidateSpells validate;
    
  

    private void OnMouseOver()
    {
        if (gameObject.name != "finalCheckpoint" && nextCheckpoint != null)
        {
            nextCheckpoint.SetActive(true);
        }
        else if (gameObject.name == "finalCheckpoint")
            validate.Finish();


    }

    public void Reset()
    {
        if (gameObject.name != "Checkpoint1")
            gameObject.SetActive(false);
    }

   
}
