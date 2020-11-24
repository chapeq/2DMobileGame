using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
   new public string name = "New Item";
    public Sprite icon = null;
    public string type;

    public virtual void Consumme()
    {
        Debug.Log("Consumme item");
    }

    public virtual int GetPtsVie()
    {
        return 0;
    }
}
