using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameObject nextCheckpoint;
    public ValidateSpells validate;
    public GameObject particle;
    bool isFinished = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "circle")
        {
            if (gameObject.name != "finalCheckpoint" && nextCheckpoint != null)
            {
                nextCheckpoint.SetActive(true);
            }
            else if (gameObject.name == "finalCheckpoint" && !isFinished)
            {
                isFinished = true;
                Instantiate(particle, transform.position, Quaternion.identity);
                validate.Finish();
            }
        }

    }

    public void Reset()
    {
        if (gameObject.name != "Checkpoint1")
            gameObject.SetActive(false);
    }

   
}
