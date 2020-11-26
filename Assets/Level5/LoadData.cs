using UnityEngine;
using System.Linq;

public class LoadData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataToSave data = SaveSystem.LoadData();

        if (data != null)
        {
            PlayerStats.instance.ptsAttaque = data.PtsAttaque;
            PlayerStats.instance.ptsDefense = data.ptsDefense;
            PlayerStats.instance.ptsVieMax = data.PVMax;
            PlayerStats.instance.ptsVie = data.ptsVie;
            PlayerStats.instance.lvl = data.lvl;
            PlayerStats.instance.UpdateUI();

            string[] equipSaved = data.equipItems.Split(',');
            for (int i = 0; i < equipSaved.Length; i++)
            {
                if (equipSaved[i] != "")
                {
                    int id = int.Parse(equipSaved[i]);
                    Item currentItem = ItemsDatabase.instance.allItems.Single(x => x.ID == id);
                    EquipementInventory.instance.Add(currentItem);
                }
            }

            string[] itemsSaved = data.inventItems.Split(',');
            for (int i = 0; i < itemsSaved.Length; i++)
            {
                if (itemsSaved[i] != "")
                {
                    int id = int.Parse(itemsSaved[i]);
                    Item currentItem = ItemsDatabase.instance.allItems.Single(x => x.ID == id);
                    Inventory.instance.Add(currentItem);
                }
            }
        }
    }

}

    

