
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        StartCoroutine(ShowTransition());
                
    }

    IEnumerator ShowTransition()
    {
        if (transitions.Length > 0)
        {
            transitions[0].SetActive(true);
            yield return new WaitForSeconds(displayTime);
            transitions[0].SetActive(false);
        }
        if (transitions.Length >1 )
        {
            transitions[1].SetActive(true);
            yield return new WaitForSeconds(displayTime);
            transitions[1].SetActive(false);
        }
        SceneManager.LoadScene(levelToLoad);
        
    }
}
