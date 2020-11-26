
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{

    public static LevelChanger instance;
    public Animator animator;
    public GameObject[] transitions;
    public float displayTime = 2f;
    private int levelToLoad;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }


    public void FadeToNextLevel ()
    {
        
        levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        animator.SetTrigger("FadeOut");
    }

    public void LoadLevel()
    {
        SaveSystem.SaveData();
        StartCoroutine(ShowTransition());
                
    }

    IEnumerator ShowTransition()
    {
        for(int i =0; i < transitions.Length; i++)
        {
            transitions[i].SetActive(true);
            Color TrueColor = transitions[i].GetComponent<Image>().color;
            Color Fade = TrueColor;
            for (float j = 0; j <= 1; j += Time.deltaTime)
            {
                Fade.a = j;
                transitions[i].GetComponent<Image>().color = Fade;
                yield return null;
            }
            yield return new WaitForSeconds(displayTime);
            for (float j = 1; j >= 0; j -= Time.deltaTime)
            {
                Fade.a = j;
                transitions[i].GetComponent<Image>().color = Fade;
                yield return null;
            }
            transitions[i].SetActive(false);
        }
       
        SceneManager.LoadScene(levelToLoad);
        
    }
}
