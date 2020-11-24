using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int ptsAttaque;
    public int ptsDefense;
    public int ptsVie;
    public int ptsVieMax;
    public int lvl; 

    public Text TextAttaque;
    public Text TextDefense;
    public Text TextVie;
 


    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    private void Start()
    {
        ptsAttaque = 0;
        ptsDefense = 0;
        ptsVieMax = 100;
        ptsVie = ptsVieMax;
        lvl = 1; 

    }

   
    public void AddPtsAttaque(int points)
    {
        ptsAttaque += points;
        TextAttaque.text = ptsAttaque.ToString();
    }

    public void AddPtsDefense(int points)
    {
        ptsDefense += points;
        TextDefense.text = ptsDefense.ToString();
    }

    public void SetPtsVie(int points)
    {
        if (points > ptsVieMax)
            ptsVie = ptsVieMax;
        ptsVie = points;
        TextVie.text = ptsVie.ToString(); 
    }


}

