using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Menu;
    public Animator audioFade;

    public int levelToload;
    public Button LoadGame;

    public AudioMixer audiomixer;
    public Slider volumeSlider;

    public GameObject transition;
 

    private void Start()
    {
        StartCoroutine(Intro());

        DataToSave data = SaveSystem.LoadData();
        if(data != null)
        {
            levelToload = data.levelProgress;
            LoadGame.interactable = true;
        }
        else
        {
            LoadGame.interactable = false;
        }

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.75f);
        audiomixer.SetFloat("mainVolume", Mathf.Log10(volumeSlider.value) * 20);
        
    }

    public void NewGameButton()
    {
        SaveSystem.DeleteData();
        StartCoroutine(TransitionTolevel(1));
    }

    public void LoadGameButton()
    {
        StartCoroutine(TransitionTolevel(levelToload));
    }

     public void OnQuitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("mainVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume",volume); 
    }

    public IEnumerator TransitionTolevel(int level)
    {
        audioFade.SetTrigger("FadeOut");

        GameObject FadePanel = GameObject.Find("BlackFade");
        FadePanel.GetComponent<Image>().enabled = true; 
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            FadePanel.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }

        if (level == 1)
        {
            audioFade.SetFloat("animSpeed",0.5f);
            transition.SetActive(true);
            Color TrueColor = transition.GetComponent<Image>().color;
            Color Fade = TrueColor;
            for (float j = 0; j <= 1; j += Time.deltaTime)
            {
                Fade.a = j;
                transition.GetComponent<Image>().color = Fade;
                yield return null;
            }
            yield return new WaitForSeconds(3f);
            for (float j = 1; j >= 0; j -= Time.deltaTime)
            {
                Fade.a = j;
                transition.GetComponent<Image>().color = Fade;
                yield return null;
            }
            transition.SetActive(false);
        }

        SceneManager.LoadScene(level);
    }

    public IEnumerator Intro()
    {
        GameObject image = GameObject.Find("IntroImage");
        if (StateMenuLaunch.instance.IsFirstLaunch)
        {
            Image introImage = image.GetComponent<Image>();
            Color TrueColor = introImage.color;
            Color Fade = TrueColor;
            for (float j = 0; j <= 1; j += Time.deltaTime)
            {
                Fade.a = j;
                introImage.color = Fade;
                yield return null;
            }
            yield return new WaitForSeconds(3f);
            for (float j = 1; j >= 0; j -= Time.deltaTime)
            {
                Fade.a = j;
                introImage.color = Fade;
                yield return null;
            }
        }
        image.SetActive(false);
        Menu.SetActive(true);
        StateMenuLaunch.instance.IsFirstLaunch = false;
    }
}
