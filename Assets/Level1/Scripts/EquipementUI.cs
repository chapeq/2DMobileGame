using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EquipementUI : MonoBehaviour
{
    public Transform itemsParent;
    EquipementInventory equipement;
    EquipementSlot[] slots;
    private GameObject invIcon;
    private int NbrClignotement = 3;

    // Start is called before the first frame update
    void Start()
    {
        equipement = EquipementInventory.instance;
        equipement.onEquipChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<EquipementSlot>();
        invIcon = GameObject.Find("inventoryImage");

    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < equipement.equipements.Count)
            {
                slots[i].AddItem(equipement.equipements[i]);
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
