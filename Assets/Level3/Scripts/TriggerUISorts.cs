using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUISorts : MonoBehaviour
{
    public GameObject UIpanel;
    public GameObject ButonManager; 
  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "PlayerHarry")
        {
            if(UIpanel != null)
            UIpanel.SetActive(true);
            if (ButonManager != null)
                ButonManager.SetActive(true);
        }
    }


}
