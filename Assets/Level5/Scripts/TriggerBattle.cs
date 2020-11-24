using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBattle : MonoBehaviour
{
    public CombatSystem combat;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerHarry")
            combat.StartBattle();
    }
}
