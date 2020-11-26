using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public class DataToSave 
{
    public static DataToSave instance;

    public int levelProgress ;
    public int PtsAttaque ;
    public int ptsDefense ;
    public int PVMax;
    public int ptsVie ;
    public int lvl ;
    public string equipItems;
    public string inventItems ;

    public DataToSave()
    {
        levelProgress = SceneManager.GetActiveScene().buildIndex;
        PtsAttaque = PlayerStats.instance.ptsAttaque;
        ptsDefense = PlayerStats.instance.ptsDefense;
        PVMax = PlayerStats.instance.ptsVieMax;
        ptsVie = PlayerStats.instance.ptsVie;
        lvl = PlayerStats.instance.lvl;
        equipItems = string.Join(",", EquipementInventory.instance.equipements.Select(x => x.ID));
        inventItems = string.Join(",", Inventory.instance.items.Select(x => x.ID));
    }
    
}
