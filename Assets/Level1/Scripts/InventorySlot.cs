using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button infoButton;
    public GameObject tooltip;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        infoButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        infoButton.interactable = false;
    }

    public void OnInfoButton()
    {
        bool isTooltipShow = false;
        if (tooltip != null && !isTooltipShow)
        {
            isTooltipShow = true;
            Text name = tooltip.transform.GetChild(0).GetComponent<Text>();
            Text HP = tooltip.transform.GetChild(1).GetComponent<Text>();
            name.text = item.name;
            HP.text = "+ " + item.GetPtsVie() + " ptsVie";
            tooltip.SetActive(true);
        }
        else if (isTooltipShow)
        {
            tooltip.SetActive(false);
            isTooltipShow = false;
        }
    }

    public void ConsummeItem()
    {
        if (item != null)
            Consumme();
           

    }

    public void Consumme()
    {
        if (Inventory.instance.IsCombatMode && Inventory.instance.cptItemConsumme >= 1)
        {
            return;
        }
        if (Inventory.instance.IsCombatMode)
            Inventory.instance.cptItemConsumme++;

        item.Consumme();
        AudioManager.instance.Play("Heal");
        Inventory.instance.Remove(item);      
    }

}
