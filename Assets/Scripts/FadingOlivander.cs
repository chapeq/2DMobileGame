using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;

public class FadingOlivander : MonoBehaviour
{
    public GameObject TileMapFade1;
    public GameObject TileMapFade2;

    private bool IsFade = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            IsFade = !IsFade;
            if (IsFade)
            {
                TileMapFade1.GetComponent<TilemapRenderer>().enabled = false;
                TileMapFade2.GetComponent<TilemapRenderer>().enabled = false;
                TileMapFade1.GetComponent<TilemapCollider2D>().enabled = false;
                TileMapFade2.GetComponent<TilemapCollider2D>().enabled = false;
            }
            else
            {
                TileMapFade1.GetComponent<TilemapRenderer>().enabled = true;
                TileMapFade2.GetComponent<TilemapRenderer>().enabled = true;
                TileMapFade1.GetComponent<TilemapCollider2D>().enabled = true;
                TileMapFade2.GetComponent<TilemapCollider2D>().enabled = true;
            }
        }

    }

   

       
    

}
