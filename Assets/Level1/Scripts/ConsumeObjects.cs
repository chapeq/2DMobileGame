using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeObjects : MonoBehaviour
{
    public ConsumableItem item;
    public PlayerStats stats;

    public void ConsumeObject()
    {
        stats.ptsAttaque += (item.PtsAttaque);
        stats.ptsDefense +=(item.PtsDefense);
        stats.ptsVie += (item.PtsVie);

    }
}
