using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUISorts : MonoBehaviour
{
    public GameObject UIpanel;
    public GameObject circle;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "PlayerHarry")
        {
            UIpanel.SetActive(true);
            circle.SetActive(true);
        }
    }


}
