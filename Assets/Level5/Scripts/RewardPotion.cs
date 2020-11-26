using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPotion : MonoBehaviour
{
    public ConsumableItem reward;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartCoroutine(AddReward());
    }

    IEnumerator AddReward()
    {
        yield return new WaitForSeconds(10f);
        Inventory.instance.Add(reward);
    }
}

