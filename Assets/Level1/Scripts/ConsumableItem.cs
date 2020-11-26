
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ConsumableItem")]

public class ConsumableItem : Item
{
    public int PtsAttaque;
    public int PtsDefense;
    public int PVMAx;
    public int PtsVie;

    public override void Consumme()
    {
        PlayerStats.instance.AddPtsAttaque(PtsAttaque);
        PlayerStats.instance.AddPtsDefense(PtsDefense);
        PlayerStats.instance.AddPVMax(PVMAx);
        PlayerStats.instance.AddPtsVie(PtsVie);
    }

   public override int GetPtsVie()
    {
        return PtsVie;
    }

}


