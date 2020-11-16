using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeObjects : MonoBehaviour
{
    public ConsumableItem item;
    public PlayerStats stats;

    public void ConsumeObject()
    {
        stats.AddPtsAttaque(item.PtsAttaque);
        stats.AddPtsDefense(item.PtsDefense);
        stats.AddPtsVie(item.PtsVie);

    }
}
