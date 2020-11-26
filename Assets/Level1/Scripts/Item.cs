using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public int ID = 0;
   new public string name = "New Item";
    public Sprite icon = null;
    public string type;

    public virtual void Consumme()
    {

    }

    public virtual int GetPtsVie()
    {
        return 0;
    }
}
