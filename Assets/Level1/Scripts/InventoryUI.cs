
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;
    private GameObject invIcon;
    private int NbrClignotement = 3;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        invIcon = GameObject.Find("inventoryImage");
    }

    void UpdateUI()
    {
        for(int i =0; i< slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                StartCoroutine(ClignoteIcon());
            }
            else
                slots[i].ClearSlot();
        }

    }

    IEnumerator ClignoteIcon()
    {
        Image graphics = invIcon.GetComponent<Image>();
        int cpt = 0;
        while (cpt < NbrClignotement)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.2f);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
            cpt += 1;
        }
    }
}
