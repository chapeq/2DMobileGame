
using UnityEngine;

public class ItemClick : MonoBehaviour
{
    public Item equip;

    public void AddToInventory()
    {
       
        Inventory.instance.Add(equip);
        GameObject Panel = transform.parent.gameObject;
        Panel.SetActive(false);
        
    }
}
