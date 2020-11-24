using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENNEMYTURN, LOST ,WON }
public class CombatSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject CanvasBattle;
    public GameObject CanvasToHide;

    public Text dialogueText;

    public UpdateEnnemyUI ennemyUI;
    public UpdateHarryUI harryUI;

    public GameObject UIInventory ;

    private GameObject player;
    private EnnemyStat ennemystat;

    private Animator animPlayer;
    private Animator animEnemy;

    private int cptAttaque = 0;
    private bool IsDefenseActive = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerHarry")
            StartBattle();
    }

    public void StartBattle()
    {
        state = BattleState.START;
        player = GameObject.Find("PlayerHarry");
        player.GetComponent<PlayerController>().canMove = false;
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

        animPlayer = player.GetComponent<Animator>();
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
        if (cptAttaque == 3)
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
        animPlayer.SetTrigger("Attack");
        ennemystat.currentHP -= PlayerStats.instance.ptsAttaque;
        ennemyUI.SetHp(ennemystat.currentHP);
        dialogueText.text = "Bravo, " + ennemystat.ennemyname + " perds " + PlayerStats.instance.ptsAttaque + "points de vie !";

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
            Inventory.instance.IsCombatMode = true;
            UIInventory.SetActive(true);

            while (UIInventory.activeSelf)
                yield return null;

             harryUI.SetHp(PlayerStats.instance.ptsVie);
             yield return new WaitForSeconds(1f);

            Inventory.instance.IsCombatMode = false;

            state = BattleState.ENNEMYTURN;
            StartCoroutine(EnnemyAttack());
        }
    }

    IEnumerator PlayerDefense()
    {
        IsDefenseActive = true;
        dialogueText.text = "Vous activez votre bouclier PROTEGO ! ";
        animPlayer.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        state = BattleState.ENNEMYTURN;
        StartCoroutine(EnnemyAttack());
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Bravo, vous avez battu " + ennemystat.ennemyname + ". Vous gagnez 1 point d'attaque !";
            PlayerStats.instance.AddPtsAttaque(1);
            if (PlayerStats.instance.ptsAttaque > 23)
            {
                PlayerStats.instance.lvl = 2;
                yield return new WaitForSeconds(2.5f);
                dialogueText.text = "Vous passez au niveau 2 !";
            }
            CanvasToHide.SetActive(true);
            CanvasBattle.SetActive(false);

            GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<PlayerController>().canMove = true;
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Vous êtes mort ... Retentez votre chance.";
            //GameOVER
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
                "Vous ne perdez que " + tempdamage + "points de vie.";
            PlayerStats.instance.SetPtsVie(PlayerStats.instance.ptsVie - tempdamage);
            IsDefenseActive = false;
        }
        else
        {
            dialogueText.text = ennemystat.ennemyname + " attaque , vous perdez " + ennemystat.damage + "points de vie...";
            PlayerStats.instance.SetPtsVie(PlayerStats.instance.ptsVie - ennemystat.damage);
        }
        harryUI.SetHp(PlayerStats.instance.ptsVie);

        if (cptAttaque == 3)
            cptAttaque = 0;

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
