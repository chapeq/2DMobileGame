﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENNEMYTURN, LOST ,WON }
public class CombatSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject CanvasBattle;
    public GameObject CanvasToHide;
    public GameObject Centaure;
    public Text dialogueText;

    public UpdateEnnemyUI ennemyUI;
    public UpdateHarryUI harryUI;

    public GameObject UIInventory ;

    private EnnemyStat ennemystat;
    private Animator animEnemy;

    private int cptAttaque = 0;
    private bool IsDefenseActive = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartBattle();
    }

    public void StartBattle()
    {
        state = BattleState.START;
        PlayerController.instance.canMove = false;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        CanvasToHide.SetActive(false);
        CanvasBattle.SetActive(true);
        ennemystat = GetComponent<EnnemyStat>();
        dialogueText.text = ennemystat.ennemyname + " vous barre le chemin ...";
        ennemyUI.SetUI(ennemystat);
        harryUI.SetUI();

        animEnemy = GetComponent<Animator>();

        switch (PlayerStats.instance.ptsDefense)
        {
            case 13:
                ennemystat.damage -= 2;
                break;
            case 15:
                ennemystat.damage -= 4;
                break;
            case 17:
                ennemystat.damage -= 6;
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(3.5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("CPT ATTAQUE : " + cptAttaque);
        if (cptAttaque >= 3)
        {
            GameObject bouton = GameObject.Find("Attaque");
            bouton.GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject bouton = GameObject.Find("Attaque");
            bouton.GetComponent<Button>().interactable = true;
        }
        dialogueText.text = "Que voulez-vous faire ?";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        cptAttaque++;
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    public void OnDefenseButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;


        StartCoroutine(PlayerDefense());
    }

    IEnumerator PlayerAttack()
    {
        PlayerController.instance.Attack();
        ennemystat.currentHP -= PlayerStats.instance.ptsAttaque;
        ennemyUI.SetHp(ennemystat.currentHP);
        dialogueText.text = "Bravo, " + ennemystat.ennemyname + " perds " + PlayerStats.instance.ptsAttaque + " points de vie !";

        yield return new WaitForSeconds(3f);

        if (ennemystat.currentHP <= 0)
        {
            animEnemy.SetTrigger("Dead");
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENNEMYTURN;
            StartCoroutine(EnnemyAttack());
        }
    }

    IEnumerator PlayerHeal()
    {
        if (Inventory.instance.items.Count == 0)
        {
            dialogueText.text = "Vous n'avez pas de potion dans votre inventaire ...";
            yield return new WaitForSeconds(1f);
            PlayerTurn();
        }
        else
        {
            if (cptAttaque == 3)
                cptAttaque = 0;

            Inventory.instance.IsCombatMode = true;
            UIInventory.SetActive(true);

            while (UIInventory.activeSelf)
                yield return null;

             harryUI.SetHp(PlayerStats.instance.ptsVie);
             yield return new WaitForSeconds(2f);

            Inventory.instance.cptItemConsumme = 0;
            Inventory.instance.IsCombatMode = false;

            state = BattleState.ENNEMYTURN;
            StartCoroutine(EnnemyAttack());
        }
    }

    IEnumerator PlayerDefense()
    {
        if (cptAttaque == 3)
            cptAttaque = 0;

        IsDefenseActive = true;
        dialogueText.text = "Vous activez votre bouclier PROTEGO ! ";
        PlayerController.instance.Attack();

        yield return new WaitForSeconds(2f);
        state = BattleState.ENNEMYTURN;
        StartCoroutine(EnnemyAttack());
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Bravo, vous avez battu " + ennemystat.ennemyname + ".Vous gagnez " +ennemystat.RewardPtsAtt +" points d'attaque et " +
                ennemystat.RewardPtsDef +" points de défense!";
            PlayerStats.instance.AddPtsAttaque(ennemystat.RewardPtsAtt);
            PlayerStats.instance.AddPtsDefense(ennemystat.RewardPtsDef);
            yield return new WaitForSeconds(2f);

            if (PlayerStats.instance.ptsAttaque < ennemystat.lvl) 
            {
                PlayerStats.instance.lvl = ennemystat.lvl;
                dialogueText.text = "Vous passez au niveau " + ennemystat.lvl + " !!";
                yield return new WaitForSeconds(2f);
            }
            CanvasToHide.SetActive(true);
            CanvasBattle.SetActive(false);

            if (gameObject.name == "Boss" && Centaure != null)
                Centaure.SetActive(true);

            PlayerController.instance.canMove = true;
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Vous êtes mort ... Retentez votre chance.";
            PlayerController.instance.Die();
            CanvasBattle.SetActive(false);

        }
    }

    IEnumerator EnnemyAttack()
    {
        animEnemy.SetTrigger("Attack");
        if (IsDefenseActive)
        {
            int tempdamage = ennemystat.damage - PlayerStats.instance.ptsDefense;
            if (tempdamage < 0)
                tempdamage = 0;
            dialogueText.text = ennemystat.ennemyname + " attaque , mais votre bouclier vous protége ! " +
                "Vous ne perdez que " + tempdamage + " points de vie.";
            PlayerStats.instance.RemovePtsVie(tempdamage);
            IsDefenseActive = false;
        }
        else
        {
            dialogueText.text = ennemystat.ennemyname + " attaque , vous perdez " + ennemystat.damage + "points de vie...";
            PlayerStats.instance.RemovePtsVie(ennemystat.damage);
        }
        harryUI.SetHp(PlayerStats.instance.ptsVie);

        yield return new WaitForSeconds(3f);

        if (PlayerStats.instance.ptsVie <= 0)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

}
