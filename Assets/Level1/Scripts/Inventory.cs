using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool IsCombatMode = false;
    public int cptItemConsumme = 0;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public List<Item> items = new List<Item>();
    public int space = 20;

    public void Add(Item item)
    {
        if (items.Count >= space)
            return;
        items.Add(item);

        if(onItemChangedCallback != null)
        onItemChangedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
