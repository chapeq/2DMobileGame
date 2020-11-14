using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/item")]
public class Item : ScriptableObject
{
   new public string name = "New Item";
    public Sprite icon = null;
}
