using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementInventory : MonoBehaviour
{
    public static EquipementInventory instance;

    public delegate void OnEquipChanged();
    public OnEquipChanged onEquipChangedCallback;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public List<Item> equipements = new List<Item>();
    public int space = 3;

    public void Add(Item item)
    {
        if (equipements.Count >= space)
            return;
        equipements.Add(item);  
        if (onEquipChangedCallback != null)
            onEquipChangedCallback.Invoke();
    }

    
}
