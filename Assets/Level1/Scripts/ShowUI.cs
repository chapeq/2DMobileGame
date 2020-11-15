using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{

    public GameObject UIpanel;

    public void ShowPanel()
    {
        UIpanel.SetActive(true);
    }

    public void HidePanel()
    {
        UIpanel.SetActive(false);
    }

}

