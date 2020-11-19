using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameObject nextCheckpoint;

    private void Start()
    {
        
           
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "circle")
        {
            Debug.Log("Validate");
            if (gameObject.name != "finalCheckpoint" && nextCheckpoint != null)
                nextcollider.enabled = true;
            else
                Debug.Log("FINI");
        }
    }*/

    private void OnMouseOver()
    {
        Debug.Log("VALIDATE");
        if (gameObject.name != "finalCheckpoint" && nextCheckpoint != null)
            nextCheckpoint.SetActive(true);
        else
            Debug.Log("FINI");
    }
}
