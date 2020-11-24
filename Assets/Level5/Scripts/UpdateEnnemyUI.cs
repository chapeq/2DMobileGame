using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateEnnemyUI : MonoBehaviour
{
    public Text nametext;
    public Text lvl;
    public Slider HPSlider;

    public void SetUI(EnnemyStat stats)
    {
        nametext.text = stats.ennemyname;
        lvl.text = "Lvl " + stats.lvl;
        HPSlider.maxValue = stats.maxHP;
        HPSlider.value = stats.currentHP;
    }

    public void SetHp(int hp)
    {
        HPSlider.value = hp;
    }
}
