using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    public float speed;                //Floating point variable to store the player's movement speed.
    public Animator animator;

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public bool canMove = true;
    Vector2 movement;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (!canMove)
        {
            animator.SetFloat("Speed", 0);
            return;
        }
            
        //Store the current horizontal input in the float moveHorizontal.   
        movement.x= Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
         movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    }
}