﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int ptsAttaque =0;
    public int ptsDefense = 0;
    public int ptsVie  = 100;
    public int ptsVieMax= 100;
    public int lvl = 1; 

    public Text TextAttaque;
    public Text TextDefense;
    public Text TextVie;
 


    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

   
    public void AddPtsAttaque(int points)
    {
        ptsAttaque += points;
        UpdateUI();
    }

    public void AddPtsDefense(int points)
    {
        ptsDefense += points;
        UpdateUI();
    }

    public void AddPtsVie(int points)
    {

        ptsVie += points;

        if (ptsVie > ptsVieMax)
            ptsVie = ptsVieMax;

        UpdateUI();
    }

    public void RemovePtsVie(int points)
    {

        ptsVie -= points;

        if (ptsVie < 0)
            ptsVie = 0;

        UpdateUI();
    }

    public void AddPVMax(int pts)
    {
        ptsVieMax += pts;
    }

    public void UpdateUI()
    {
        TextAttaque.text = ptsAttaque.ToString();
        TextDefense.text = ptsDefense.ToString();
        TextVie.text = ptsVie.ToString();
    }
}

