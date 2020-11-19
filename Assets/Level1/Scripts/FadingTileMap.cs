
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadingTileMap : MonoBehaviour
{
    public GameObject TileMapFade1;
    public GameObject TileMapFade2;

    private Tilemap tilemap1;
    private Tilemap tilemap2;
    private Color DefaultColor;
    private Color FadeColor;
    private SpriteRenderer sprite;


    private void Start()
    {
        tilemap1 = TileMapFade1.GetComponent<Tilemap>();
        tilemap2 = TileMapFade2.GetComponent<Tilemap>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        DefaultColor = tilemap1.color;
        FadeColor = DefaultColor;
        FadeColor.a = 0.2f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            tilemap1.color = FadeColor;
            TileMapFade1.GetComponent<TilemapCollider2D>().enabled = false;
            tilemap2.color = FadeColor;
            TileMapFade2.GetComponent<TilemapCollider2D>().enabled = false;
            if(sprite != null)
            sprite.color = FadeColor;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            tilemap1.color = DefaultColor;
            TileMapFade1.GetComponent<TilemapCollider2D>().enabled = true;
            tilemap2.color = DefaultColor;
            TileMapFade2.GetComponent<TilemapCollider2D>().enabled = true;
            if (sprite != null)
             sprite.color = DefaultColor;
        }

    }






}
