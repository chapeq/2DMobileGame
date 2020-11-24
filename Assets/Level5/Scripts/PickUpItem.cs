using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public ConsumableItem item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerHarry")
            TakeItem();
    }

    void TakeItem()
    {
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
