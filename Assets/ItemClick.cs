
using UnityEngine;

public class ItemClick : MonoBehaviour
{
    public Item item;

    public void AddToInventory()
    {
        Debug.Log("Click detecté");
        Inventory.instance.Add(item);
        GameObject Panel = transform.parent.gameObject;
        Debug.Log("Panel a cacher :" + Panel.name);
        Panel.SetActive(false);

    }
}
