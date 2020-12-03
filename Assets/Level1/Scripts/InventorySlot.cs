using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public GameObject tooltip;
   

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

   

    public void SelectItem()
    {
        if (item != null)
        {
            Text info = tooltip.transform.GetChild(0).GetComponent<Text>();
            Button valider = tooltip.transform.GetChild(1).GetComponent<Button>();
            if (Inventory.instance.IsCombatMode && Inventory.instance.cptItemConsumme >= 1)
            {
                info.text = "Vous ne pouvez pas boire plus d'une potion lors d'un combat !";
                valider.interactable = false;
                
            }
            else
            {
                info.text = "Voulez-vous boire " + item.name + "? Cela vous rendra " + item.GetPtsVie() + "pts de vie ! ";
                valider.interactable = true;
                valider.onClick.RemoveAllListeners();
                valider.onClick.AddListener(() => { Consumme(); });
            }
            tooltip.SetActive(true);
        }
        else
            return;
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
