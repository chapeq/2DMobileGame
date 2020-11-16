
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public static LevelChanger instance;
    public Animator animator;
    public GameObject transition;
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
        if (transition != null)
        {
            transition.SetActive(true);
            yield return new WaitForSeconds(3f);
            transition.SetActive(false);
        }
        SceneManager.LoadScene(levelToLoad);
        
    }
}
