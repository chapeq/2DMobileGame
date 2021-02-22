using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button infoButton;
    public GameObject tooltip;
    private bool ShowTooltip = true;

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
        Debug.Log(ShowTooltip);
        if (tooltip != null && ShowTooltip)
        {
            ShowTooltip = false;
            Text name = tooltip.transform.GetChild(0).GetComponent<Text>();
            Text HP = tooltip.transform.GetChild(1).GetComponent<Text>();
            name.text = item.name;
            HP.text = "+ " + item.GetPtsVie() + " ptsVie";
            tooltip.SetActive(true);
        }
        else if (!ShowTooltip)
        {
            tooltip.SetActive(false);
            ShowTooltip = true;
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
