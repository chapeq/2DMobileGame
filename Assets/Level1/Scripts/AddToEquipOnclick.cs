
using UnityEngine;

public class AddToEquipOnclick : MonoBehaviour
{
    public Item equip;

    public void AddToInventory()
    {
        EquipementInventory.instance.Add(equip);
        GameObject Panel = transform.parent.parent.gameObject;
        Destroy(Panel);
        
    }

}
