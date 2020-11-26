
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public Item[] allItems;

    public static ItemsDatabase instance;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
}
