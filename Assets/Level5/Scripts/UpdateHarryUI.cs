using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHarryUI : MonoBehaviour
{
    public Text nametext;
    public Text lvl;
    public Slider HPSlider;

    public void SetUI()
    {
        nametext.text = "HARRY";
        lvl.text = "Lvl " + PlayerStats.instance.lvl;
        HPSlider.maxValue = PlayerStats.instance.ptsVieMax;
        HPSlider.value = PlayerStats.instance.ptsVie;
    }

    public void SetHp(int hp)
    {
        HPSlider.value = hp;
    }
}
