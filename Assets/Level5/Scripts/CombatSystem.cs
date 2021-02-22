using System.Collections;
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

    public GameObject AttackVFX;
    public GameObject ShieldVFX;

    private EnnemyStat ennemystat;
    private Animator animEnemy;

    private int cptAttaque = 0;
    private bool IsDefenseActive = false;
    private AudioSource Mainaudio;
    private GameObject spell;
    private GameObject shield;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartBattle();
    }

    public void StartBattle()
    {
        state = BattleState.START;
        PlayerController.instance.canMove = false;
        Mainaudio = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        Mainaudio.Pause();
        if (gameObject.name == "Boss")
            AudioManager.instance.Play("BossBattle");
        else
            AudioManager.instance.Play("BattleTheme");
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

        yield return new WaitForSeconds(3f);

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
        spell = Instantiate(AttackVFX, PlayerController.instance.transform.position, Quaternion.identity);
        PlayerController.instance.Attack();
        ennemystat.currentHP -= PlayerStats.instance.ptsAttaque;
        ennemyUI.SetHp(ennemystat.currentHP);
        dialogueText.text = "Bravo, " + ennemystat.ennemyname + " perds " + PlayerStats.instance.ptsAttaque + " points de vie !";

        yield return new WaitForSeconds(3f);

        Destroy(spell);

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
            cptAttaque = 0;

            Inventory.instance.IsCombatMode = true;
            UIInventory.SetActive(true);

            while (UIInventory.activeSelf)
                yield return null;

             harryUI.SetHp(PlayerStats.instance.ptsVie);
             yield return new WaitForSeconds(1f);

            Inventory.instance.cptItemConsumme = 0;
            Inventory.instance.IsCombatMode = false;

            state = BattleState.ENNEMYTURN;
            StartCoroutine(EnnemyAttack());
        }
    }

    IEnumerator PlayerDefense()
    {
        cptAttaque = 0;

        IsDefenseActive = true;
        dialogueText.text = "Vous activez votre bouclier PROTEGO ! ";
        PlayerController.instance.Attack();
        shield = Instantiate(ShieldVFX, PlayerController.instance.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        state = BattleState.ENNEMYTURN;
        StartCoroutine(EnnemyAttack());
    }

    IEnumerator EndBattle()
    {

        if (gameObject.name == "Boss") 
            AudioManager.instance.Stop("BossBattle");
        else
            AudioManager.instance.Stop("BattleTheme");
       
        if (state == BattleState.WON)
        {
            AudioManager.instance.Play("Victory");
            dialogueText.text = "Bravo, vous avez battu " + ennemystat.ennemyname + ".Vous gagnez " + ennemystat.RewardPtsAtt +" points d'attaque ..." ;
            if (gameObject.name == "Boss")
            {
                yield return new WaitForSeconds(1f);
                dialogueText.text =  "..et " + ennemystat.RewardPtsDef + " points de défense!! ";
                PlayerStats.instance.AddPtsDefense(ennemystat.RewardPtsDef);
            }
            PlayerStats.instance.AddPtsAttaque(ennemystat.RewardPtsAtt);
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
            Mainaudio.UnPause();

        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Vous êtes mort ... Retentez votre chance.";
            StartCoroutine(PlayerController.instance.Die());
            CanvasBattle.SetActive(false);

        }
    }

    IEnumerator EnnemyAttack()
    {
        animEnemy.SetTrigger("Attack");
        AudioManager.instance.Play("EnemyAttack");
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
        Destroy(shield);
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
