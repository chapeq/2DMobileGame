using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button removeButton;
    public GameObject tooltip;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void ConsummeItem()
    {
        if (item != null)
            StartCoroutine(TooltipConsumme());
           

    }

    IEnumerator TooltipConsumme()
    {
        if (tooltip != null)
        {
            Text name = tooltip.transform.GetChild(0).GetComponent<Text>();
            Text HP = tooltip.transform.GetChild(1).GetComponent<Text>();
            name.text = item.name;
            HP.text = "+ " + item.GetPtsVie() + " ptsVie";
            tooltip.SetActive(true);
            yield return new WaitForSeconds(2f);
            tooltip.SetActive(false);
        }
        if (Inventory.instance.IsCombatMode && Inventory.instance.cptItemConsumme >= 1)
        {
            yield break;
        }
        if (Inventory.instance.IsCombatMode)
            Inventory.instance.cptItemConsumme++;

        item.Consumme();

        Inventory.instance.Remove(item);
        
    }

}
