using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector timeline;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();

    }

    public void StartTimeline()
    {
        timeline.Play();
    }
}
