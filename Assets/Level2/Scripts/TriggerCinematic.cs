using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TriggerCinematic : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject fadeOut;
    private PlayerController playerMove;
    private bool asStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerHarry")
        {
            playerMove = collision.GetComponent<PlayerController>();
            playerMove.canMove = false;
            StartCoroutine(FadeOut());
        }

    }

    IEnumerator FadeOut()
    {
        fadeOut.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            fadeOut.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }
        fadeOut.SetActive(false);
        timeline.Play();
        asStarted = true;
    }

    private void Update()
    {
        if (asStarted)
        {
            if (timeline.state != PlayState.Playing)
                LevelChanger.instance.FadeToNextLevel();
        }
    }
}
