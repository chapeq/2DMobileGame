using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelOntrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "PlayerHarry")
            LevelChanger.instance.FadeToNextLevel();
    }
}
