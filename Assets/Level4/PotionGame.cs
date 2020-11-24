using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.PackageManager.Requests;

public class PotionGame : MonoBehaviour
{
    public GameObject Grille;
    public GameObject panel;
    public GameObject TriggerZone;
    public GameObject TriggerLevel;
    public List<Button> _listButtons;

    private List<Button> _buttonsSelected = new List<Button>();
    private Button[] allIngredients;
    private int tour = 0; 
    

    // Start is called before the first frame update
    void Start()
    {
       
        allIngredients = Grille.GetComponentsInChildren<Button>();
       for (int i = 0; i < allIngredients.Length; i++)
        {
            Button but = allIngredients[i];
            allIngredients[i].onClick.AddListener(() => { AddButton(but); });
          
        }
        StartCoroutine(HighlightButton());
    }

 
    void AddButton(Button boutonCliked)
    {
        StopAllCoroutines();
        Debug.Log("Bouton Clicker :  " + boutonCliked);
        _buttonsSelected.Add(boutonCliked);
        StartCoroutine(Timer());
    }

    bool CheckList()
    {
        for( int i = 0; i< _buttonsSelected.Count; i++ )
        {
            if (_listButtons[i] != _buttonsSelected[i])
                return false;
        }
        return true; 
    }
 

    IEnumerator HighlightButton()
    {
        StopCoroutine(Timer());
        for (int i = 0; i < allIngredients.Length; i++)
        {
            allIngredients[i].interactable = false;
        }
        yield return new WaitForSeconds(1f);

        for (int i = 0; i <= tour ; i++)
        {
            Debug.Log("Bouton HighLight : " + _listButtons[i]);
            Image bouton = _listButtons[i].GetComponent<Image>();
            bouton.color = Color.blue;
            yield return new WaitForSeconds(1f);
            bouton.color = Color.white;
        }
        for (int i = 0; i < allIngredients.Length; i++)
        {
            allIngredients[i].interactable = true;
        }
    }

    IEnumerator Timer()
    {
            Debug.Log("start timer");
        yield return new WaitForSeconds(tour * 1f);
        if (!CheckList())
        {
            tour = 0;
            _buttonsSelected.Clear();
            StartCoroutine(HighlightButton());
        }
        else if (CheckList() && _buttonsSelected.Count == _listButtons.Count)
            Finish();
        else if (CheckList() && _buttonsSelected.Count != _listButtons.Count)
        {
            tour++;
            _buttonsSelected.Clear();
            StartCoroutine(HighlightButton());
        }
    }

    void Finish()
    {
        Debug.Log("FINISH");
        Destroy(panel);
        GetComponent<DialogTrigger>().DisplayOnclick();
        if (TriggerZone != null)
            Destroy(TriggerZone);
        if (TriggerLevel != null)
            TriggerLevel.SetActive(true);
    }
}
