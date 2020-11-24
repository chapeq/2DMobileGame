
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ConsumableItem")]

public class ConsumableItem : Item
{
    public int PtsAttaque;
    public int PtsDefense;
    public int PtsVie;
 

    public override void Consumme()
    {
        Debug.Log("Consumme item 2");
        PlayerStats.instance.AddPtsAttaque(PtsAttaque);
        PlayerStats.instance.AddPtsDefense(PtsDefense);
        PlayerStats.instance.SetPtsVie(PlayerStats.instance.ptsVie + PtsVie);
    }

    public override int GetPtsVie()
    {
        return PtsVie;
    }

}


