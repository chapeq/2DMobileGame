using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class VoldemortBattle : MonoBehaviour
{
    public BattleState state;

    public GameObject VoldemortImage;
    public GameObject VoldemMort;
    public GameObject Dumbledore;
    public GameObject CanvasBattle;
    public GameObject CanvasToHide;
    public Text dialogueText;

    public UpdateEnnemyUI ennemyUI;
    public UpdateHarryUI harryUI;

    public GameObject UIInventory;

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
        AudioManager.instance.Play("BattleTheme");
        StartCoroutine(ShowPanel());
    }

    IEnumerator ShowPanel()
    {
        VoldemortImage.SetActive(true);
        Color TrueColor = VoldemortImage.GetComponent<Image>().color;
        Color Fade = TrueColor;
        for (float j = 0; j <= 1; j += Time.deltaTime)
        {
            Fade.a = j;
            VoldemortImage.GetComponent<Image>().color = Fade;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        for (float j = 1; j >= 0; j -= Time.deltaTime)
        {
            Fade.a = j;
            VoldemortImage.GetComponent<Image>().color = Fade;
            yield return null;
        }
        VoldemortImage.SetActive(false);
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        CanvasToHide.SetActive(false);
        CanvasBattle.SetActive(true);
        ennemystat = GetComponent<EnnemyStat>();
        dialogueText.text = ennemystat.ennemyname + " vous confronte...";
        ennemyUI.SetUI(ennemystat);
        harryUI.SetUI();

        animEnemy = GetComponent<Animator>();

        switch (PlayerStats.instance.ptsDefense)
        {
            case 23:
                ennemystat.damage -= 2;
                break;
            case 25:
                ennemystat.damage -= 4;
                break;
            case 27:
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

        AudioManager.instance.Stop("BattleTheme");

        if (state == BattleState.WON)
        {
            animEnemy.SetTrigger("Dead");
            AudioManager.instance.Play("Victory");
            dialogueText.text = "Bravo, vous avez vaincu " + ennemystat.ennemyname + "!!";
            yield return new WaitForSeconds(2f);
            StartCoroutine(ShowDeathImage());
            CanvasToHide.SetActive(true);
            CanvasBattle.SetActive(false);
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

    IEnumerator ShowDeathImage()
    {
        VoldemMort.SetActive(true);
        Color TrueColor = VoldemMort.GetComponent<Image>().color;
        Color Fade = TrueColor;
        for (float j = 0; j <= 1; j += Time.deltaTime)
        {
            Fade.a = j;
            VoldemMort.GetComponent<Image>().color = Fade;
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);
        for (float j = 1; j >= 0; j -= Time.deltaTime)
        {
            Fade.a = j;
            VoldemMort.GetComponent<Image>().color = Fade;
            yield return null;
        }
        Dumbledore.SetActive(true);
        VoldemMort.SetActive(false);      
    }
}
