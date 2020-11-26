using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;               
    public Animator animator;
    public bool canMove = true;

    private Rigidbody2D rb;        
    private Vector2 movement;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (!canMove)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        movement.x = Input.GetAxis("Horizontal");
         movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
   
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Die()
    {
        animator.SetTrigger("Dead");
        GameOver.instance.ShowGameOver();
    }

}