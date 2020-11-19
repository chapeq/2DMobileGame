using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private GameObject Harry;
    private void Awake()
    {
        Harry = GameObject.Find("PlayerHarry");
        if (Harry != null)
        {
            Harry.transform.position = transform.position;
            Debug.Log("position Harry: " + Harry.transform.position);
            Debug.Log("position Spawn : " + transform.position);
        }
    }
}
