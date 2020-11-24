using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameObject nextCheckpoint; 
    public ValidateSpells validate;
    
   
    private void Start()
    {
       
    }

    private void OnMouseOver()
    {
        if (gameObject.name != "finalCheckpoint" && nextCheckpoint != null)
        {
            nextCheckpoint.SetActive(true);
            Debug.Log("VALIDATE" + gameObject.name);
        }
        else if (gameObject.name == "finalCheckpoint")
            validate.Finish();


    }

    public void Reset()
    {
        Debug.Log("Reset checkpoint");
        if (gameObject.name != "Checkpoint1")
            gameObject.SetActive(false);
    }

   
}
