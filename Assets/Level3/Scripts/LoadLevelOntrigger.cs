using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelOntrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            LevelChanger.instance.FadeToNextLevel();
    }
}
