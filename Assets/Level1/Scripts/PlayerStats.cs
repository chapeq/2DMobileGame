using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int ptsAttaque;
    private int ptsDefense;
    private int ptsVie;
    public Text TextAttaque;
    public Text TextDefense;
    public Text TextVie;


    private void Start()
    {
        ptsAttaque = 0;
        ptsDefense = 0;
        ptsVie = 100;
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

    public void AddPtsVie(int points)
    {
        ptsVie += points;
        TextVie.text = ptsVie.ToString();
    }

    public void RemovePtsVie(int points)
    {
        ptsVie -= points;
        TextVie.text = ptsVie.ToString();
    }


}

