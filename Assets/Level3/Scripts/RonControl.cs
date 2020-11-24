using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonControl : MonoBehaviour
{
    public float Speed = 5f;

    private Animator animator;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        GameObject p = GameObject.Find("PlayerHarry");
        if (p != null)
            player = GameObject.Find("PlayerHarry").transform;

    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position,
                                                     Speed * Time.deltaTime);
        }

    }

    void FixedUpdate()
    {
        Vector2 Movement = new Vector2(transform.position.x, transform.position.y);
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Vertical", transform.position.y);
        animator.SetFloat("Speed", Movement.sqrMagnitude);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerHarry")
            Destroy(gameObject.GetComponent<BoxCollider2D>());
    }
}
