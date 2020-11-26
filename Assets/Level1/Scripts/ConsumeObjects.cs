using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeObjects : MonoBehaviour
{
    public ConsumableItem item;
    public PlayerStats stats;

    public void ConsumeObject()
    {
       PlayerStats.instance.AddPtsAttaque(item.PtsAttaque);
       PlayerStats.instance.AddPtsDefense(item.PtsDefense);
    }
}
